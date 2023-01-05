using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class getSelectables : MonoBehaviour
{
    public static float selectionRadius = 3f;
    public List<GameObject> attached = new List<GameObject>();
    public KeyCode AttachKey;
    public KeyCode DettachKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //LayerMask mask = LayerMask.GetMask("selectable");
        //if (Input.GetKeyDown(DettachKey))
        //{

        //    if (attached.Count <= 0) return;
        //    var toDetach= attached[0];
        //    toDetach.gameObject.PublishDettachCaller(toDetach.gameObject, gameObject);
        //    return;
        //}
        //var colliders = Physics.OverlapSphere(transform.position, selectionRadius, mask);
        //if (colliders.Length <= 0)
        //{
        //    return;
        //}
        //var Selected = colliders.Where((c)=> !attached.Contains(c.gameObject)).OrderBy((c)=> Vector3.Distance(transform.position, c.transform.position)).FirstOrDefault();
        //if (Selected == null) return;
        //if (Input.GetKeyDown(AttachKey)) {
        //    Selected.gameObject.PublishAttachCaller(Selected.gameObject, gameObject);
        //    return;
        //}
        //Selected.gameObject.PublishSelect(gameObject);
    }
}
