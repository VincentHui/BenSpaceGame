using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnMouseEvents : MonoBehaviour
{
    SpringVector3 scale;
    Vector3 scaleTarget;
    // Start is called before the first frame update
    void Start()
    {
        var initialScale = new Vector3();
        scaleTarget = new Vector3(1f, 1f, 1f);
        scale = gameObject.useSpringVec3((s)=> { transform.localScale = s.position; }, initialScale, scaleTarget);
        gameObject.SubscribeBroker("ENTER",()=> {
            Debug.Log("ENTER");
            scale.target = Vector3.Max(scaleTarget * 1.5f , new Vector3(2,2,2));
        });
        gameObject.SubscribeBroker("EXIT", () => {
            Debug.Log("EXIT");
            scale.target = new Vector3(1f,1f,1f);
        });
        gameObject.SubscribeBroker("HOVER", () => {

            //Debug.Log("HOVER");
        });
        gameObject.SubscribeBroker("POINTER_DOWN", () => {
            Debug.Log("POINTER DOWN");
            scale.target = Vector3.Max(scaleTarget * 1.5f, new Vector3(3, 3, 3));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
