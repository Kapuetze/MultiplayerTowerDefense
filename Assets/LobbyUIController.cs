using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClosePanel(GameObject target)
    {
        if (target.activeInHierarchy)
            target.gameObject.SetActive(false);
        else
            return;
    }

    public void OpenPanel(GameObject target)
    {
        if (!target.activeInHierarchy)
            target.gameObject.SetActive(true);
        else
            return;
    }
}
