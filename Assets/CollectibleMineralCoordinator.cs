using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMineralCoordinator : MonoBehaviour
{
    rigidBodyFollower rope;
    tractorBeam beam;
    // Start is called before the first frame update
    void Start()
    {
        rope = GetComponent<rigidBodyFollower>();
        beam = GetComponent<tractorBeam>();
        gameObject.SubscribeBroker<GameObject>("ATTACH", (msg) => {
            //Debug.Log("SUBSCRIBED ATTACH");
            rope.toFollow = msg.What;
            beam.AttachBeam(msg.What.transform);
            //msg.Who.GetComponent<>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
