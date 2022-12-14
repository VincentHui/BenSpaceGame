using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIEventEmitter))]
public class selectAnimation : MonoBehaviour
{
   // SpringVector3 scaleSpring;
    // Start is called before the first frame update
    void Start()
    {
        SpringVector3 scaleSpring = gameObject.useSpringVec3(scale => transform.localScale = scale.position);
        scaleSpring.position = new Vector3(1,1,1);
        scaleSpring.target = new Vector3(1, 1, 1);
        gameObject.SubscribeBroker("SELECT", () => scaleSpring.target = new Vector3(1.2f, 1.2f, 1.2f));
        //gameObject.SubscribeBroker("SUBMIT", () => scaleSpring.target = new Vector3(0.9f, 0.9f, 0.9f));
        gameObject.SubscribeBroker("DESELECT", () => scaleSpring.target = new Vector3(1, 1, 1));
    }
}
