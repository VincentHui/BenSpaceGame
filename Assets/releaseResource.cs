using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class releaseResource : ParticleBulletPoolBase
{
    public float releaseImpulse = 20f;
    // Start is called before the first frame update
    void Start()
    {
        MakePool(10);
        gameObject.SubscribeBroker<MinableCollisionInfo>("MINABLE_COLLISION", (obj) => {


            //for (int i = 0; i < 10; i++)
            //{
            if (!pool.CanGetFromPool) return;
            var particle = Spawn();
            particle.bulletObject.transform.position = obj.What.hit.point;
            //particle.bulletObject.transform.localScale *= Random.Range(0.2f, 1.4f);
            //var speed = Random.Range(0.001f, 0.09f);
            var normal = obj.What.hit.normal;
            normal.y = 0;
            normal.Normalize();
                

            var newDirection = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * normal;
            particle.bulletObject.GetComponent<steerable>().AddConsumableSteeringAction((body)=> newDirection * releaseImpulse);
            //}
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
