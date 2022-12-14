using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringYvalue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var initialValue = transform.position.y;
        var spring = gameObject.useSpring(target: initialValue, friction: 12f, tension: 180f, position: initialValue -2, mass: 9f, callback: (s) => {
            transform.position = new Vector3(transform.position.x, s.position, transform.position.z);
        });
        gameObject.SubscribeGlobal("FADE_MARKER", () => {
            spring.target = initialValue + 2;
        });

        gameObject.SubscribeGlobal("FOCUS_MARKER", () => {
            spring.target = initialValue;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
