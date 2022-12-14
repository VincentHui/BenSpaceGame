using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConsumableUpdaterExtension
{
    private static consumableUpdater scheduler;

    public static consumableUpdater MakeScheduler(string name = "scheduler")
    {
        var holderObj = new GameObject(name);
        return holderObj.AddComponent<consumableUpdater>();
    }

    public static void AddToGlobalUpdater(this GameObject p_obj, System.Action OnStart, System.Action<float> OnUpdate, System.Action OnEnd, float Length, string schedulerName = "scheduler")
    {
        scheduler = scheduler ? GameObject.Find(schedulerName).GetComponent<consumableUpdater>() : MakeScheduler(schedulerName);
        scheduler.AddToUpdater(OnStart, OnUpdate, OnEnd, Length);
    }
    public static void AddToGlobalUpdater(this GameObject p_obj, System.Action OnStart, System.Action OnEnd, float Length, string schedulerName = "scheduler")
    {
        AddToGlobalUpdater(p_obj, OnStart, (t) => { }, OnEnd, Length, schedulerName);
    }

    public static void AddToGlobalUpdater(this GameObject p_obj, System.Action OnEnd, float Length, string schedulerName = "scheduler")
    {
        AddToGlobalUpdater(p_obj, () => { }, (t) => { }, OnEnd, Length, schedulerName);
    }
}

public class consumableUpdater : MonoBehaviour
{
    List<Task> tasks = new List<Task>();
    public void AddToUpdater(System.Action OnStart, System.Action<float> OnUpdate, System.Action OnEnd, float Length)
    {
        Task newTask = new Task();
        newTask.OnStart = OnStart;
        newTask.OnUpdate = OnUpdate;
        newTask.OnEnd = OnEnd;
        newTask.length = Length;
        tasks.Add(newTask);
    }
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
            if (tasks[i].elapsed <= 0)
            {
                tasks[i].OnStart();
            }
            tasks[i].elapsed += Time.deltaTime;
            tasks[i].OnUpdate(tasks[i].elapsed);
            if (tasks[i].elapsed >= tasks[i].length)
            {
                tasks[i].OnEnd();
                tasks.RemoveAt(i);
            }
        }
    }
}
