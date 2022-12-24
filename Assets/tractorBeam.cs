using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractorBeam : MonoBehaviour
{
    public Transform A, B;
    public GameObject particle;
    //ObjectPool<GameObject> beamParticles;
    List<GameObject> active = new List<GameObject>();
    public int amount = 10;
    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < amount; i++)
        {
            var toAdd = Instantiate(particle);
            active.Add(toAdd);
            toAdd.transform.position = A.position;
            toAdd.transform.position = Vector3.Lerp(A.position, B.position, 1f/amount * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //var dist = Vector3.Distance(A.position, B.position);
        for (int i = 0; i < amount; i++)
        {
            var particleObj = active[i];
            particleObj.transform.position = A.position;
            particleObj.transform.position = Vector3.Lerp(A.position, B.position, i * 1f / amount);
        }
        //foreach (GameObject obj in active) {

        //}
    }
}
