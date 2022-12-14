using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringToTarget : MonoBehaviour
{
    public GameObject follow;
    SpringVector3 postionSpring;
    // Start is called before the first frame update
    public float tension = 1f;
    public float mass = 1f;
    public float friction = 1f;
    //public Vector3 target = new Vector3();
    void Start()
    {
        postionSpring = new SpringVector3
        {
            position = follow.transform.position,
            target = transform.position + (Vector3.left * 10),            
        };
        //target = postionSpring.target;
    }

    // Update is called once per frame
    void Update()
    {
        postionSpring.target = follow.transform.position;
        postionSpring.mass = mass;
        postionSpring.friction = friction;
        postionSpring.tension = tension;
        postionSpring.update(Time.deltaTime);
        transform.position = postionSpring.position;
    }
}
