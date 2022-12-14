using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAlpha : MonoBehaviour
{
    Color color;
    TextMesh tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMesh>();
        color = tm.color;
        //color = tm.color;
        //color.a = 0;
        //tm.color = color;


        var spring = gameObject.useSpring(target: 1, tension: 12f, position: 0, callback: (s) => {

           //Debug.Log(s.position);
            color = tm.color;
            color.a = s.position;
            tm.color = color;

        });

        gameObject.SubscribeGlobal("FADE_MARKER",()=> {
            //Debug.Log("FADE");
            spring.target = 0;
        });

        gameObject.SubscribeGlobal("FOCUS_MARKER", () => {
            spring.target = 1;
        });
        //color.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
