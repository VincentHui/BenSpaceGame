using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class springFollower : MonoBehaviour
{
    public GameObject toFollow;
    public SpringVector3 spring;
    public float radius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        spring.position = transform.position;
        spring.target = toFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (transform.position - toFollow.transform.position).normalized;
        var distance = Vector3.Distance(toFollow.transform.position, transform.position);
        spring.target = toFollow.transform.position;
        spring.tension = Mathf.Max(distance - radius, 0) / radius;
        spring.update(Time.deltaTime);
        transform.position = spring.position;
    }
}
