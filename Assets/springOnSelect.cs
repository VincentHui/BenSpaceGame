using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springOnSelect : MonoBehaviour
{

    Spring scale;
    GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        scale = gameObject.useSpring((s)=> {
            transform.localScale = new Vector3(s.position, s.position, s.position);
        }, 1, 1);
        //scale.mass = 1;
        //scale.tension = 3;
        gameObject.SubscribeBroker<GameObject>("SELECT",(sender)=> {
            scale.target = 1.5f;
            selector = sender.What;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!selector) return;
        if (Vector3.Distance(selector.transform.position, transform.position) > getSelectables.selectionRadius)
        {
            scale.target = 1f;
            selector = null;
        }

    }
}
