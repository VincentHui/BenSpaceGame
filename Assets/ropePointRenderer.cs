using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropePointRenderer : MonoBehaviour
{
    public GameObject p1, p2;
    public springFollower spring;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPositions(new Vector3[] { p1.transform.position, spring.spring.position });
    }

    //private void OnEnable()
    //{
    //    Application.onBeforeRender += UpdateRoute;
    //}

    //private void OnDisable()
    //{
    //    Application.onBeforeRender -= UpdateRoute;
    //}

    //public void UpdateRoute()
    //{
    //    lr.SetPositions(new Vector3[] { p1.transform.position, p2.transform.position });
    //}

    // Update is called once per frame
    void Update()
    {
        lr.SetPositions(new Vector3[] { p1.transform.position, spring.spring.position });
    }
}
