using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.GraphicsBuffer;

public class springFollower : MonoBehaviour
{
    public GameObject toFollow;
    public SpringImpulseVector3 spring;
    public float radius = 10f;
    public LayerMask collideWith;

    // Start is called before the first frame update
    void Start()
    {
        spring.position = transform.position;
        spring.target = toFollow.transform.position;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("COLLECTIBLE HIT");
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("COLLECTIBLE HIT");
    //}

    // Update is called once per frame
    void Update()
    {
        //var direction = (transform.position - toFollow.transform.position).normalized;
        var distance = Vector3.Distance(toFollow.transform.position, transform.position);
        spring.target = toFollow.transform.position;
        spring.tension = Mathf.Max(distance - radius, 0) / radius;

        //Vector3 deltaMovement = spring.Velocity * Time.deltaTime;

        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, collideWith);
        //if (hitColliders.Length > 0) {
        //    spring.impulse = () =>
        //    {
        //        var force = Vector3.zero;
        //        for (int i = 0; i < hitColliders.Length; i++)
        //        {
        //            var dir = (transform.position - hitColliders[i].transform.position).normalized;
        //            force += dir *spring.Velocity.magnitude * 1f;
        //        }
        //        //var result = (transform.position - hitColliders[0].transform.position).normalized * spring.Velocity.magnitude * 1f;
        //        return force;
        //    };
        //}


        spring.update(Time.deltaTime);
        transform.position = spring.position;
    }
}
