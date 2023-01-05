using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConsumablePredicateExtension
{
    private static ConsumablePredicateUpdate scheduler;
    public static ConsumablePredicateUpdate MakePredicateUpdater()
    {
        var holderObj = new GameObject(nameof(ConsumablePredicateUpdate));
        return holderObj.AddComponent<ConsumablePredicateUpdate>();
    }

    public static void AddToPredicateUpdater(this GameObject p_obj, Func<bool> predicateUpdate)
    {
        scheduler = scheduler ? GameObject.Find(nameof(ConsumablePredicateUpdate))
            .GetComponent<ConsumablePredicateUpdate>() : MakePredicateUpdater();
        scheduler.addToTask(predicateUpdate);
    }

}
public class ConsumablePredicateUpdate : MonoBehaviour
{
    public List<Func<bool>> tasks = new List<Func<bool>>();

    public void addToTask(Func<bool> toAdd) => tasks.Add(toAdd);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.Count <= 0)
        {
            return;
        }
        for (int i = tasks.Count - 1; i >= 0; i--)
        {
            var result = tasks[i]();
            if(result)tasks.RemoveAt(i);
        }
    }
}
