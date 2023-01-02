using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustOnCollision : ManualParticleSystemBase
{
    //public GameObject DustObject;
    public float timeToLive = 2f;
    // Start is called before the first frame update
    void Start()
    {
        MakePool(200);
        gameObject.SubscribeBroker<MinableCollisionInfo>("MINABLE_COLLISION", (obj) => {
            for (int i=0; i < 10; i++) {
                var particle = Spawn(timeToLive);
                particle.sprite.transform.position = obj.What.hit.point;
                particle.sprite.transform.localScale *= Random.Range(0.2f,1.4f);
                var speed = Random.Range(0.001f, 0.09f);
                var materialCol = particle.sprite.GetComponent<MeshRenderer>().material.color;
                var normal = obj.What.hit.normal;
                normal.y = 0;
                normal.Normalize();

                var newDirection = Quaternion.AngleAxis(Random.Range(-45f, 45f), Vector3.up) * normal;

                gameObject.AddToGlobalUpdater(() => {

                }, (t) => {
                    particle.sprite.transform.position += speed.LerpDuration(t, 0f, timeToLive) * newDirection;
                    particle.sprite.GetComponent<MeshRenderer>().material.color =
                        new Color(materialCol.r, materialCol.g, materialCol.b, (1f).LerpDuration(t,0f,timeToLive));
                }, () => {

                }, timeToLive);
            }
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
