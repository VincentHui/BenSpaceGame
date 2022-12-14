using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputMessager : MonoBehaviour
{
    //Spring postion = new Spring();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.PublishBroker("LEFT");
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.PublishBroker("RIGHT");
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.PublishBroker("UP");
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.PublishBroker("DOWN");
        }
    }
}
