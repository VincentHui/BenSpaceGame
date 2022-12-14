using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerableDampened : steerableBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;
        Vector3 tempVelocity = Vector3.zero;
        var _enemerator = actionMap.GetEnumerator();
        tempVelocity = iterateActions(tempVelocity, _enemerator);
        var _consumableEnem = ConsumableActionMap.GetEnumerator();
        tempVelocity = iterateConsumable(tempVelocity, _consumableEnem, ConsumablesToRemove);
        applyForce(tempVelocity);
        _velocity += acceleration;
    }
}
