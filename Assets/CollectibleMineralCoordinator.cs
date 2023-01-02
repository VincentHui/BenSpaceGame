using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMineralCoordinator : MonoBehaviour
{
    rigidBodyFollower rigidBodyFollower;
    tractorBeam beam;
    public Item<GameObject> itemOnAttach = ItemPouch.pouch["CollectibleAsteroid"];
    public Item<GameObject> itemOnDettach = ItemPouch.pouch["DropAsteroidItem"];

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyFollower = GetComponent<rigidBodyFollower>();
        beam = GetComponent<tractorBeam>();
        gameObject.SubscribeBroker<GameObject>("ATTACH", (msg) => {
            rigidBodyFollower.toFollow = msg.What;
            beam.AttachBeam(msg.What.transform);
            msg.What.BuyItem(itemOnAttach);
        });
        gameObject.SubscribeBroker<GameObject>("DETTACH", (msg) => {
            rigidBodyFollower.toFollow = null;
            beam.DettachBeam();
            msg.What.BuyItem(itemOnDettach);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
