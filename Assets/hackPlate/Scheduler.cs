using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task {
    public float length;
    public float elapsed=0f;
    public System.Action OnStart;
    public System.Action OnEnd;
    public System.Action<float> OnUpdate;
}

public static class SchedulerExtension {
    private static Scheduler scheduler;

    public static Scheduler MakeScheduler(string name= "scheduler")
    {
        var holderObj = new GameObject(name);
        return holderObj.AddComponent<Scheduler>();
    }

    public static void AddToSchedule(this GameObject p_obj, System.Action OnStart, System.Action<float> OnUpdate, System.Action OnEnd, float Length, string schedulerName = "scheduler") {
        scheduler = scheduler ? GameObject.Find(schedulerName).GetComponent<Scheduler>() : MakeScheduler(schedulerName);
        scheduler.AddToSchedule(OnStart, OnUpdate, OnEnd, Length);
    }
    public static void AddGlobalSchedule(this GameObject p_obj, System.Action OnStart, System.Action OnEnd, float Length, string schedulerName = "scheduler")
    {
        AddToSchedule(p_obj, OnStart, (t) => { }, OnEnd,  Length, schedulerName);
    }

    public static void AddGlobalSchedule(this GameObject p_obj, System.Action OnEnd, float Length, string schedulerName = "scheduler")
    {
        AddToSchedule(p_obj, ()=> { }, (t) => { }, OnEnd, Length, schedulerName);
    }
}

public class Scheduler : MonoBehaviour
{
    Queue<Task> myQ = new Queue<Task>();

    public void AddToSchedule(System.Action OnStart, System.Action<float> OnUpdate, System.Action OnEnd, float Length) {
        Task newTask = new Task();
        newTask.OnStart = OnStart;
        newTask.OnEnd = OnEnd;
        newTask.length = Length;
        newTask.OnUpdate = OnUpdate;
        myQ.Enqueue(newTask);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myQ.Count <= 0) {
            return;
        }
        var current =  myQ.Peek();
        if (current.elapsed <= 0)
        {
            current.OnStart();
        }
        current.elapsed += Time.deltaTime;
        current.OnUpdate(current.elapsed);
        if (current.elapsed >= current.length)
        {
            current.OnEnd();
            myQ.Dequeue();
        }
    }
}
