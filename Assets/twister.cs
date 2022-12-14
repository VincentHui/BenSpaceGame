using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twister : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //elapsedTime += Time.deltaTime;
        transform.Rotate(new Vector3(0,1,0), Time.deltaTime * 20f);
        transform.Rotate(new Vector3(1, 0, 0), Time.deltaTime * 20f);
    }
}
