using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class getSelectables : MonoBehaviour
{
    public static float selectionRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = LayerMask.GetMask("selectable");
        var colliders = Physics.OverlapSphere(transform.position, selectionRadius, mask);
        if (colliders.Length <= 0)
        {
            return;
        }
        var col = colliders.OrderBy((c)=> Vector3.Distance(transform.position, c.transform.position)).FirstOrDefault();
        col.gameObject.PublishBroker("SELECT", gameObject);
        //Debug.Log(col.name);
    }
}
