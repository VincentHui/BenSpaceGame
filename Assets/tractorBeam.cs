using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractorBeam : MonoBehaviour
{
    public Transform toBeam;
    public GameObject particle;
    //ObjectPool<GameObject> beamParticles;
    List<GameObject> active = new List<GameObject>();
    public int amount = 10;

    public void AttachBeam(Transform toAttach) {
        active.ForEach(obj => {
            obj.SetActive(true);
        });
        toBeam = toAttach;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var toAdd = Instantiate(particle);
            toAdd.SetActive(false);
            active.Add(toAdd);
            //toAdd.transform.position = toBeam.position;
            //toAdd.transform.position = Vector3.Lerp(transform.position, toBeam.position, 1f/amount * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toBeam == null)
        {
            return;
        }
        for (int i = 0; i < amount; i++)
        {
            var particleObj = active[i];
            particleObj.transform.position = transform.position;
            particleObj.transform.position = Vector3.Lerp(transform.position, toBeam.transform.position, i * 1f / amount);
        }
    }
}
