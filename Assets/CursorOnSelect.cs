using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;

public class CursorOnSelect : MonoBehaviour
{
    ObjectPool<GameObject> pool;
    public KeyCode attachKey;
    public GameObject cursorSegment;
    GameObject selector;
    // Start is called before the first frame update
    void Start()
    {

        pool = cursorSegment.MakePool(5);
        ObjectPool<GameObject>.swapLists(pool.Inactive, pool.Active);
        foreach (var obj in pool.Active)
        {
            obj.SetActive(true);
        }

        gameObject.SubscribeBroker<GameObject>("SELECT", (sender) => {
            selector = sender.What;
            var points = gameObject.GetPointsOnCircle(5, 2).ToArray();
            var objects = pool.Active;
            for (int i = 0; i < 5; i++)
            {
                objects[i].SetActive(true);
                objects[i].transform.position = points[i];
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!selector) return;
        var points = gameObject.GetPointsOnCircle(5, 2).ToArray();
        var objects = pool.Active;
        if (Vector3.Distance(selector.transform.position, transform.position) > getSelectables.selectionRadius)
        {
            selector = null;
            for (int i = 0; i < 5; i++)
            {
                objects[i].SetActive(false);
            }
            return;
        }
        for (int i=0; i < 5; i++) {
            objects[i].transform.position = points[i];
        }
        if (Input.GetKeyDown(attachKey)) {
            //Debug.Log("ATTACH" + gameObject.name);
            gameObject.PublishBroker("ATTACH", selector);
        }
        
    }
}
