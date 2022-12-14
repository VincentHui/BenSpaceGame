using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public struct LerpTimeStruct {
    public Vector3 value;
    public float timePassed;
}

public static class SpriteExtension {
    //public static LerpTimeStruct LerpOverTime(Vector3 A, Vector3 B, float duration, float timePassed = 0) {
    //    float time = timePassed;
    //    //Vector3 startPosition = transform.position;
    //    while (time < duration)
    //    {
    //        var value = Vector3.Lerp(A, B, time / duration);
    //        time += Time.deltaTime;
    //        //yield return null;
    //    }
    //    //transform.position = targetPosition;
    //    return new LerpTimeStruct() { value= value, timePassed=timePassed  };
    //}
}

public class SpriteEffect: MonoBehaviour {
    protected void RecycleSprite() {
        Destroy(gameObject);
    }
}

public class sprite3D : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.forward = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
