using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteTimeToLive : SpriteEffect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float time = 0;
    // Update is called once per frame
    void Update()
    {
        //var value = Vector3.Lerp(A, B, time / duration);
        time += Time.deltaTime;
        if (time > 5) {
            RecycleSprite();
        }
    }
}
