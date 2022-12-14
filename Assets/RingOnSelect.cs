using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOnSelect : MonoBehaviour
{
    Spring radiusSpring;
    public GameObject ringDebris;
    public int amountOfDebris = 5;
    private float fullAngle = Mathf.PI* 2f;
    private List<GameObject> debris = new List<GameObject>();
    GameObject selector;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfDebris; i++)
        {
            var newDebris = Instantiate(ringDebris);
            debris.Add(newDebris);
            newDebris.transform.position = transform.position + new Vector3(getSelectables.selectionRadius, 0, 0);
            newDebris.transform.parent = transform;
            var x = transform.position.x + getSelectables.selectionRadius * Mathf.Cos(i == 0 ? 0 : (fullAngle / amountOfDebris) * i);
            var y = transform.position.z + getSelectables.selectionRadius * Mathf.Sin(i == 0 ? 0 : (fullAngle / amountOfDebris) * i);
            newDebris.transform.position = new Vector3(x, 0, y);

        }
        radiusSpring = gameObject.useSpring((s) => {
            var radius = getSelectables.selectionRadius + radiusSpring.position;
            for (int i = 0; i < amountOfDebris; i++)
            {
                var debrisToChange = debris[i];
                debrisToChange.transform.position = transform.position + new Vector3(getSelectables.selectionRadius, 0, 0);
                var x = transform.position.x + radius * Mathf.Cos(i == 0 ? 0 : (fullAngle / amountOfDebris) * i);
                var y = transform.position.z + radius * Mathf.Sin(i == 0 ? 0 : (fullAngle / amountOfDebris) * i);
                debrisToChange.transform.position = new Vector3(x, 0, y);
            }
        }, 0, 0);
        gameObject.SubscribeBroker<GameObject>("SELECT", (sender) => {
            radiusSpring.target = 0.5f;
            selector = sender.What;
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (!selector) return;
        if (Vector3.Distance(selector.transform.position, transform.position) > getSelectables.selectionRadius)
        {
            radiusSpring.target = 0f;
            selector = null;
        }
    }
}
