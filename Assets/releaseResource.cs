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

            if (!pool.CanGetFromPool) return;
            var particle = Spawn();
            particle.bulletObject.transform.position = obj.What.hit.point;
            var pos = particle.bulletObject.transform.position;
            //particle.bulletObject.transform.localScale *= Random.Range(0.2f, 1.4f);
            //var speed = Random.Range(0.001f, 0.09f);
            var normal = obj.What.hit.normal;
            normal.y = 0;
            normal.Normalize();
                
            var newDirection = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.up) * normal;
            particle.bulletObject.AddToGlobalUpdater(() => { }, (t) => {
                
                particle.bulletObject.transform.position = pos.LerpDuration(pos + normal *1f,t, 1f);
            }, () => { }, 1f);

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
