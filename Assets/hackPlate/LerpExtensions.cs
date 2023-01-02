using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LerpExtension
{

    public static float LerpDuration(this float startValue, float timeElapsed, float endValue, float lerpDuration)
    {
        return (timeElapsed < lerpDuration) ? Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration) : endValue;
    }

    public static Vector3 LerpDuration(this Vector3 startValue, Vector3 endValue, float timeElapsed, float lerpDuration)
    {
        return (timeElapsed < lerpDuration) ? Vector3.Lerp(startValue, endValue, timeElapsed / lerpDuration) : endValue;
    }

    public static float LerpInRange(float min, float max, float t) => Mathf.Clamp(min * (1 - t) + max * t, min, max);

    public static float ExtLerpInRange(this float t, float min, float max) => LerpInRange(min,max,t);

    public static float Invlerp (float min, float max, float t) => Mathf.Clamp((t - min) / (max - min), 0, 1);

    public static float ExtInvlerp(this float t, float min, float max) => Invlerp(min, max, t);

}

public class LerpExtensions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
