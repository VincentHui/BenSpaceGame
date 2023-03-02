using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using static PoximityInteractionExtension;

public static class PoximityInteractionExtension
{
    public static readonly float Radius = 5f;
    public enum ProximityEvent
    {
        ATTACH,
        DETTACH,
        SELECT,
        UNSELECT
    }
    public enum ProximityState {
        ATTACH,
        DETTACH,
        SELECT,
        UNSELECT
    }
    public static readonly string CollectibleTagName = nameof(ProximitySelectible);
    public static void SubscribeSelect(this GameObject obj, Action<MessagePayload<GameObject>> _selectCallback)
    {
        obj.SubscribeBroker<GameObject>("SELECT", _selectCallback);
    }

    public static void SubscribeProximityEvent(this GameObject obj, Action<GameObject, ProximityEvent> publish) {
        var collectible = obj.GetComponent<ProximitySelectible>();
        collectible = collectible == null ? obj.AddComponent<ProximitySelectible>() : collectible;
        collectible.addProximityCallback(publish);
    }


}
public class ProximitySelector : MonoBehaviour
{
    public readonly string tagName = PoximityInteractionExtension.CollectibleTagName;
    public GameObject selected;
    public List<GameObject> selectionExclusion = new List<GameObject>();

    void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(transform.position, PoximityInteractionExtension.Radius);
        var Selected = colliders.Where((c) => c.gameObject.tag == tagName && selectionExclusion.Contains(c.gameObject) ==false).OrderBy((c) => Vector3.Distance(transform.position, c.transform.position)).ToList();
        selected = Selected.Count > 0 ? Selected[0].gameObject : null;
        if (selected == null) return;
        selected.PublishBroker<ProximitySelector>("SELECT", this);


    }
}

public class ProximitySelectible : MonoBehaviour
{
    public readonly string tagName = PoximityInteractionExtension.CollectibleTagName;
    private List<Action<GameObject, ProximityEvent>> ProximityCallbacks = new List<Action<GameObject, ProximityEvent>>();
    public void addProximityCallback(Action<GameObject, ProximityEvent> callback) => ProximityCallbacks.Add(callback);
    private ProximitySelector collector;

    void FireCallBack(GameObject sender, ProximityEvent _event) => ProximityCallbacks.ForEach(action => action(sender, _event));

    void Awake()
    {
        gameObject.SubscribeBroker<GameObject>("ATTACH", (m) => {
            //Collector = m.What;
            FireCallBack(gameObject, ProximityEvent.ATTACH);
        });
        gameObject.SubscribeBroker("DETTACH", () => {
            //Collector = null;
            FireCallBack(gameObject, ProximityEvent.DETTACH);
        });
        gameObject.SubscribeBroker<ProximitySelector>("SELECT", (m) =>
        {
            if (collector == null) {
                collector = m.What;
                FireCallBack(gameObject, ProximityEvent.SELECT);
            }

        });
        gameObject.tag = tagName;
    }
    void Update()
    {
        if (collector == null) return;
        if (collector.selected != gameObject) {
            collector = null;
            FireCallBack(gameObject, ProximityEvent.UNSELECT);

        }
    }
}