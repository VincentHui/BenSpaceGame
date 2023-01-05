using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static PoximityInteractionExtension;
using static UnityEngine.GridBrushBase;

public class CursorOnSelect : MonoBehaviour
{
    ObjectPool<GameObject> pool;

    public GameObject cursorSegment;
    //GameObject selector;

    void SetSelected(bool value) {
        pool.Active.ForEach(obj=>obj.SetActive(value));
    }

    // Start is called before the first frame update
    void Start()
    {

        pool = cursorSegment.MakePool(5);
        ObjectPool<GameObject>.swapLists(pool.Inactive, pool.Active);
        SetSelected(false);

        gameObject.SubscribeProximityEvent((sender, _event) => {
            if (_event == ProximityEvent.SELECT)
            {
                SetSelected(true);
            }
            else {
                SetSelected(false);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        var points = gameObject.GetPointsOnCircle(5, 2).ToArray();
        var objects = pool.Active;
        for (int i = 0; i < 5; i++)
        {
            objects[i].transform.position = points[i];
        }
    }
}
