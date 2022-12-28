using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class rigidBodyFollower : MonoBehaviour
{
    public GameObject toFollow;
    public SpringImpulseVector3 spring;
    public float radius = 10f;
    public LayerMask collideWith;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        spring.position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toFollow == null)
            return;
        var distance = Vector3.Distance(toFollow.transform.position, transform.position);
        var direction = (toFollow.transform.position - transform.position).normalized;
        spring.target = toFollow.transform.position;
        spring.position = rigidBody.position;
        spring.tension = Mathf.Max(distance - radius, 0) / radius;
        spring.update(Time.deltaTime);
        rigidBody.velocity = spring.Velocity;

    }
}
