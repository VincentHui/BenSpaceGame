using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInventoryEffects : MonoBehaviour
{
    public Dictionary<string, int> effectsLedger;
    public readonly Dictionary<string, Action<GameObject, int>> dispatchTable=
        new Dictionary<string, Action<GameObject, int>>() {
        ["CollectibleWeightEffect"] = (obj, effect) => {
            var weightEffectValue = LerpExtension.Invlerp(0f,5f, effect)* 1.1f;
            obj.GetComponent<steerable>().RemoveSteeringAction("GravityEffect");
            obj.GetComponent<steerable>().AddSteeringAction("GravityEffect", (s) => s.velocity * -weightEffectValue);
        }
    };
    public inventory _inventory;
    // Start is called before the first frame update
    void Start()
    {
        effectsLedger = gameObject.getInventory();
        _inventory = GetComponent<inventory>();
        //GetComponent<steerable>().AddSteeringAction("GravityEffect", SeekDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inventory._effectsLedger != effectsLedger) {
            effectsLedger = _inventory._effectsLedger;
            foreach (var effect in effectsLedger) {
                if (dispatchTable.ContainsKey(effect.Key)) {
                    dispatchTable[effect.Key](gameObject,effect.Value);
                }
            }
        }
    }

    Vector3 SeekDirection(steerableBase p_steerable)
    {
        return p_steerable.velocity*-0.6f;
    }
}
