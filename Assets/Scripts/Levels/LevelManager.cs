using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Utilities.EventSystem;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private float _spawnDelay;
        [SerializeField]
        private float _waveDelay;


        private Level _currentLevel;
        private GameObject[] _creepPrefabs;
        private int _waveCounter;
        private Wave _currentWave;
        private Queue<Creep> _creepWave;




        void Start()
        {
            _creepPrefabs = Resources.LoadAll<GameObject>("Creeps");

            //Load level from xml
            TextAsset xmlAsset = Resources.Load<TextAsset>("levels/level");
            using (StringReader reader = new StringReader(xmlAsset.text))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Level));
                _currentLevel = serializer.Deserialize(reader) as Level;
            }
            _waveCounter = 0;


        }

        public void StartSpawner()
        {
			Debug.Log("Start Spawner");
			StartCoroutine(Spawner());
        }

        public void StopSpawner()
        {
            StopCoroutine(Spawner());
        }

        IEnumerator Spawner()
        {
            foreach (Wave wave in _currentLevel.Waves)
            {
                _currentWave = wave;
                _creepWave = new Queue<Creep>(_currentWave.Creeps);

                yield return new WaitForSeconds(_waveDelay);

                while (_creepWave.Count > 0)
                {
                    Creep creep = _creepWave.Dequeue();
                    int amount = int.Parse(creep.Amount);

                    for (int i = 0; i < amount; i++)
                    {
                        GameObject instant = Instantiate(_creepPrefabs[int.Parse(creep.Id)], _spawnPoint.transform.position, Quaternion.identity);
                        NetworkServer.Spawn(instant);
                        GameEvents.current.OnCreepSpawn();

                        yield return new WaitForSeconds(_spawnDelay);
                    }
                }
                _waveCounter++;
            }
            
            yield return null;
        }

    }
}