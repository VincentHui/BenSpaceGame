using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpringImageAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var image = GetComponent<RawImage>();
        var imageAlphaSpring = gameObject.useSpring((s) =>
        {
            image.color = new Color(image.color.r, image.color.b, image.color.g, s.position);
        }, 0, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
