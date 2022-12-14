using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public struct MinableCollisionInfo {
    public steerable colliderSteerable;
    public RaycastHit hit;
}

public class minableCollision : MonoBehaviour
{
    public LayerMask collideWith;

    steerable steer;
    // Start is called before the first frame update
    void Start()
    {
        steer = GetComponent<steerable>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = steer.velocity * Time.deltaTime;
        var tempRay = new Ray(transform.position, transform.TransformVector(Vector3.forward));
        //var skinValue = Mathf.Clamp(Vector3.Magnitude(deltaMovement), 0.1f,0.2f) ;
        //Debug.DrawLine(transform.position , transform.position + transform.TransformVector( Vector3.forward * skinValue) , Color.red);
        RaycastHit hit;
        var _raycastHit = Physics.Raycast(tempRay, out hit, Mathf.Clamp(Vector3.Magnitude(deltaMovement)*10f,0.2f, 1f), collideWith);
        if (_raycastHit)
        {
            gameObject.PublishBroker("MINABLE_COLLISION", new MinableCollisionInfo()
            {
                colliderSteerable = steer,
                hit = hit
            });

            hit.collider.gameObject.PublishBroker("MINABLE_COLLISION", new MinableCollisionInfo()
            {
                colliderSteerable = steer,
                hit = hit
            });

        }
    }
}
