using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParticleControl : MonoBehaviour
{
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        //particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //particles.Play();
        gameObject.SubscribeGlobal<Vector3>("PLAYER_MOVE", (itm) =>
        {

            ////Debug.Log(itm.What);
            //var emission = particles.emission;
            //if (itm.What == Vector3.zero)
            //{
            //    //var emission = particles.emission;
            //    emission.enabled = false;
            //    //particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            //    return;
            //}
            ////Debug.Log("play");

            //emission.enabled = true;
            //particles.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            //Debug.Log("start");
            var emission = particles.emission;
            emission.enabled = true;

            //particles.Play();
            //if (!particles.isPlaying)
            //{

            //    //particles.Emit(2);
            //    //particles.Play();
            //}

        }
        else
        {
            //if (!particles.isStopped)
            //{
            //    Debug.Log("stop");
            //    //particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            //}
            var emission = particles.emission;
            emission.enabled = false;
        }
    }
}
