using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.


public class UIGroupEmitter : MonoBehaviour
{
    List<Transform> children = new List<Transform>();
    // Start is called before the first frame update

    void SelectChildren() {
        Debug.Log($"{gameObject.name} : SELECT");
    }

    void DeSelectChildren()
    {

    }
    void Awake() {
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        gameObject.SubscribeBroker("START_GROUP", () => {
            children.ForEach((child) => child.gameObject.SetActive(true));
            gameObject.PublishBroker("START_GROUP", children);
        });
    }



}