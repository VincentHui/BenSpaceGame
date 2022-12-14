using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleSpriteMovement : MonoBehaviour
{
    public Vector3 Direction = Vector3.forward;
    public float velocity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.forward * Time.deltaTime * velocity);
    }
}
