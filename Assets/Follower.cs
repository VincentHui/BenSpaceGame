using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    //public Vector3 target;
    public Transform target;
    public SpringVector3 positionSpring;
    // Start is called before the first frame update
    void Start()
    {
        positionSpring =gameObject.useSpringVec3((s)=> transform.position = s.position, transform.position, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        positionSpring.target = target.position;
    }
}
