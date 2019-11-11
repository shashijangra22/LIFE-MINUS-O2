using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float bound = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > bound || transform.position.z > bound || transform.position.x < -bound || transform.position.z < -bound || transform.position.y < -bound)
        {
            Destroy(gameObject);
        }
    }
}
