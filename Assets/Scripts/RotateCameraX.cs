using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 200;
    public GameObject player;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            //transform.Rotate(Vector3.up,horizontalInput * rotationSpeed * Time.deltaTime);
            player.transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
    }
}
