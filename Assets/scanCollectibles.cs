using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scanCollectibles : MonoBehaviour
{
    public float collectionRadius = 5f;
    private GameObject toDetachFrom;
    //public List<GameObject> toDetachFrom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Collect(GameObject obj) {
        obj.PublishBroker("DETTACH");

    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = LayerMask.GetMask("selectable");
        var newRay = new Ray(transform.position + Vector3.up * 20, Vector3.down);
        var colliders = Physics.SphereCastAll(newRay, collectionRadius, 20);
        if (colliders.Length <= 2)
        {
            return;
        }
        if (colliders.Length == 1 && colliders[0].collider.gameObject == gameObject)
        {
            return;
        }
        //if (toDetachFrom == null)
        //{
        //    //colliders.Where((col) => col);
        //}

        foreach (var col in colliders) {
            //Debug.Log(col.collider.name);
            Collect(col.collider.gameObject);
        }

    }
}
