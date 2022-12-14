using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineImpulseOnCollide : MonoBehaviour
{
    public float impulseValue = 25f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SubscribeBroker<MinableCollisionInfo>("MINABLE_COLLISION", (obj) => {
            obj.What.colliderSteerable.AddConsumableSteeringAction((s) =>
            {
                var opposingDirection = new Vector3(obj.What.hit.normal.x, 0, obj.What.hit.normal.z);
                var vel = obj.What.colliderSteerable.velocity.magnitude;
                obj.What.colliderSteerable.velocity = Vector3.zero;
                
                //Debug.Log(vel);
                return opposingDirection.normalized * impulseValue * s.MaxForce;
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
