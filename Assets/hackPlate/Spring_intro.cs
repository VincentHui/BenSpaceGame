using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var initialScale = transform.localScale;
        gameObject.useSpringVec3((s)=> {
            transform.localScale = s.position;
        }, Vector3.zero, initialScale, 2f);
    }

    public void Exit() {
        var targetPos = transform.position + new Vector3(-200,0,0);
        var initialPos = transform.position;
        gameObject.useSpringVec3((p) => { transform.position = p.position; }, initialPos, targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
