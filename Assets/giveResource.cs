using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Parabola
{
    public static Vector3 arc3D(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}

public struct ResourceTransaction {
    public Dictionary<string, int> values;
    public GameObject from;
    public GameObject to;
}

public class giveResource : MonoBehaviour
{
    public GameObject mineral;
    GameObject selector;
    GameObject newMineral;
    float timeElapsed = 0f;
    float speed = 1.1f;
    public string nameOfResource ="defaultResource";
    //public int mineralsLeft = 1000;
    // Start is called before the first frame update
    void Start()
    {
        //dir = goal - transform.position;
        newMineral = Instantiate(mineral);
        newMineral.SetActive(false);
        gameObject.SubscribeBroker<GameObject>("SELECT", (sender) => {
            selector = sender.What;
            newMineral.SetActive(true);
        });
    }

    void sendMineral(Vector3 origin, Vector3 target, float timeElapsed, GameObject mineral) {
        mineral.transform.position = Parabola.arc3D(origin, target, 2f, timeElapsed * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (selector == null) return;
        timeElapsed += Time.deltaTime;
        sendMineral(transform.position,selector.transform.position, timeElapsed, newMineral);
        if (newMineral.transform.position.y < -0.5f) {

            timeElapsed = 0;
            newMineral.SetActive(false);

            //gameObject.startTransaction(new Dictionary<string, InventoryValue>() { [nameOfResource]=1 }, gameObject, selector);

            selector = null;
        }
    }
}
