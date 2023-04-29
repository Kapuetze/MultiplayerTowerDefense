using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed = 0.1f;
    public float scrollSpeed = 5f;


    private void Start()
    {

    }

    void Update () 
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            transform.position += Time.deltaTime * moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {

            transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed; 
        }
    }


}