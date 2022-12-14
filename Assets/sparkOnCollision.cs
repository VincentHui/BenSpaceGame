using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class sparkOnCollision : MonoBehaviour
{
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var Shape = particle.shape;
        gameObject.SubscribeBroker<MinableCollisionInfo>("MINABLE_COLLISION", (obj) => {
            var normal = obj.What.hit.normal;
            normal.y = 0;
            normal.Normalize();
            Shape.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(normal),
                1f).eulerAngles;
            //Shape.position = obj.What.hit.point;
            particle.Emit(20);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
