using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameLimiter : MonoBehaviour
{
    public int frameRate = 45;
    void Awake()
    {
#if UNITY_EDITOR
     QualitySettings.vSyncCount = 0;  // VSync must be disabled
     Application.targetFrameRate = frameRate;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
