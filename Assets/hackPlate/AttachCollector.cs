using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCollector : ProximitySelector
{
    public KeyCode attachKey;
    public KeyCode dettachKey;
    public List<GameObject> attached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (selected)
        {
            if (Input.GetKeyDown(attachKey))
            {
                selected.PublishBroker("ATTACH", gameObject);
                selectionExclusion.Add(selected);
                attached.Add(selected);
            }
        }
        if (attached.Count > 0) {
            if (Input.GetKeyDown(dettachKey))
            {
                attached[0].PublishBroker("DETTACH", gameObject);
                selectionExclusion.Remove(attached[0]);
                attached.RemoveAt(0);
            }
        }

    }
}
