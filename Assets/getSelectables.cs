using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class SelectableExtensions {
    public static void SubscribeSelect(this GameObject obj, Action<MessagePayload<GameObject>> _selectCallback) {
        obj.SubscribeBroker<GameObject>("SELECT", _selectCallback);
    }

    public static void PublishSelect(this GameObject obj, GameObject sender) => obj.gameObject.PublishBroker("SELECT", sender);

    public static void PublishAttachCaller(this GameObject obj, GameObject toAttach, GameObject toBeAttachedTo) {
        toBeAttachedTo.GetComponent<getSelectables>().attached.Add(toAttach);
        toAttach.PublishBroker("ATTACH", toBeAttachedTo);

    }

    public static void SubscribeAttach(this GameObject obj, GameObject toAttach, Action<MessagePayload<GameObject>> _attachCallback)
    {
        //toBeAttachedTo.GetComponent<getSelectables>().attached.Add(toAttach);
        toAttach.SubscribeBroker("ATTACH", _attachCallback);

    }

    public static void PublishDettachCaller(this GameObject obj, GameObject toDettach, GameObject toBeDetachedFrom)
    {
        toBeDetachedFrom.GetComponent<getSelectables>().attached.Remove(toDettach);
        toDettach.PublishBroker("DETTACH", toBeDetachedFrom);

    }

}

public class getSelectables : MonoBehaviour
{
    public static float selectionRadius = 3f;
    public List<GameObject> attached;
    public KeyCode AttachKey;
    public KeyCode DettachKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = LayerMask.GetMask("selectable");
        if (Input.GetKeyDown(DettachKey))
        {

            if (attached.Count <= 0) return;
            Debug.Log("Dettach");
            var toDetach= attached[0];
            toDetach.gameObject.PublishDettachCaller(toDetach.gameObject, gameObject);
            return;
        }
        var colliders = Physics.OverlapSphere(transform.position, selectionRadius, mask);
        if (colliders.Length <= 0)
        {
            return;
        }
        var Selected = colliders.Where((c)=> !attached.Contains(c.gameObject)).OrderBy((c)=> Vector3.Distance(transform.position, c.transform.position)).FirstOrDefault();
        if (Selected == null) return;
        //col.gameObject.PublishBroker("SELECT", gameObject);
        if (Input.GetKeyDown(AttachKey)) {
            Debug.Log("Attach");
            Selected.gameObject.PublishAttachCaller(Selected.gameObject, gameObject);
            return;
        }
        Selected.gameObject.PublishSelect(gameObject);
        //Debug.Log(col.name);
    }
}
