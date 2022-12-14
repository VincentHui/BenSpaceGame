using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    GameObject selector;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SubscribeBroker<GameObject>("SELECT", (sender) => {
            selector = sender.What;
            if (Merchant.isWalletsEmpty(
                new Dictionary<string, int>[] { selector.getInventory() })
            ) {
                return;
            }
            gameObject.startTransaction(
                selector.getInventory(),
                selector,
                gameObject);

        });
    }

    // Update is called once per frame
    void Update()
    {
        if (selector == null) return;
        if (Vector3.Distance(selector.transform.position, transform.position) > getSelectables.selectionRadius)
        {
            selector = null;
        }
    }
}
