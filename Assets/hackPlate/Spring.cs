using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct springParam
{
    public float position;
    public float target;
    public float tension;
}

public static class SpringExtensions
{
    public static SpringHolder MakeSpringHolder()
    {
        var holderObj = new GameObject("Spring Holder");
        return holderObj.AddComponent<SpringHolder>();
    }
    private static SpringHolder springHolder;
    public static Spring useSpring(this GameObject p_obj, Action<Spring> callback, float position = 0, float target = 0, float tension = 1f,float friction=1f, float mass=1f, bool pause = false)
    {
        var spring = new Spring
        {
            position = position,
            target = target,
            tension = tension,
            friction = friction,
            callback = callback,
            mass= mass,
            state = SpringState.MOVING
        };
        springHolder = springHolder ? GameObject.Find("Spring Holder").GetComponent<SpringHolder>() : MakeSpringHolder();
        springHolder.holder.Add(spring);
        return spring;
    }

    public delegate void useSpringsDelegate<T>(Spring x, T y);

    public static Springs useSprings<T>(this GameObject p_obj, List<T> list, useSpringsDelegate<T> callback
    , useSpringsDelegate<T> init, float delay = 0.5f, float position = 0, float target = 0, float tension = 5f)
    {
        return useSprings<T>(p_obj, list, callback, init, delay, position, tension);
    }



    public static Springs useSprings<T>(this GameObject p_obj, List<T> list, useSpringsDelegate<T> callback,
    useSpringsDelegate<T> init,
    float delay,
     float position, float tension)
    {
        List<Spring> result = new List<Spring>();
        foreach (var item in list)
        {

            Spring scaleSpring = p_obj.useSpring(s =>
            {
                callback(s, item);
            }, position, position, tension, pause: true);
            init(scaleSpring, item);
            result.Add(scaleSpring);
        }
        var springs = new Springs(result);
        springHolder.CoroutineStart(Springs.chain(springs._Springs, delay));
        return springs;
    }


    public static SpringVector3 useSpringVec3(this GameObject p_obj, Action<SpringVector3> callback, Vector3 initialPosition = default(Vector3), Vector3 target = default(Vector3), float tension = 5f, float friction = 1f, float mass = 1f)
    {
        var spring = new SpringVector3
        {
            position = initialPosition,
            target = target,
            tension = tension,
            friction = friction,
            mass = mass,
            callback = callback
        };
        springHolder = springHolder ? GameObject.Find("Spring Holder").GetComponent<SpringHolder>() : MakeSpringHolder();
        springHolder.holder.Add(spring);
        return spring;
    }

}
public class SpringHolder : MonoBehaviour
{
    public List<BaseSpring> holder = new List<BaseSpring>();
    public int test =9;
    void Update()
    {
        for (int i = holder.Count - 1; i >= 0; i--)
        {
            if (holder[i].state == SpringState.DELETE) {
                holder.RemoveAt(i);
            }
        }
        for (int i = 0; i < holder.Count; i++)
        {
            holder[i].update(Time.deltaTime);
        }

    }

    public void CoroutineStart(IEnumerator coroutineMethod) {
        Debug.Log("Start");
        StartCoroutine(coroutineMethod);
    }
}

public enum SpringState {
    REACHED,
    MOVING,
    PAUSED,
    DELETE
} 


public class Springs {
    private List<Spring> springs;

    public Springs(List<Spring> result)
    {
        //waitTime = waitTime;
        springs = result;
    }

    public List<Spring> _Springs{
        get {
            return springs;
        }
    }
    public static IEnumerator chain(List<Spring> items, float waitTime)
    {
        foreach (var item in items)
        {
            yield return new WaitForSeconds(waitTime);
            item.state = SpringState.PAUSED;
        }
    }

}

public abstract class BaseSpring
{
    public abstract void update(float delta);

    public float mass = 1f;
    public float tension = 1f;
    public float friction = 1f;
    public SpringState state;
}

[System.Serializable]  // You need to have this line in there
public class Spring : BaseSpring
{
    public float target = 1.0f;
    public float position = 0f;
    private float velocity = 0f;
    public Action<Spring> callback = (pos) => { };
    public Action<Spring> onEnd = (pos) => { };

    public override void update(float delta)
    {

        if (state == SpringState.PAUSED || state == SpringState.DELETE)
        {
            return;
        }
        if (Mathf.Abs(target - position) <= 0.01f)
        {
            position = target;
            if (state == SpringState.MOVING)
            {
                state = SpringState.REACHED;
                onEnd(this);
            }
            return;
        }
        //reached = false;
        state = SpringState.MOVING;
        var force = -tension * (position - target);
        var damping = -friction * velocity;
        var acceleration = (force + damping) / mass;
        velocity = velocity + acceleration;
        position = position + velocity * delta;
        callback(this);
    }
}

[System.Serializable]
public class SpringVector3 : BaseSpring
{

    public Vector3 target = new Vector3();
    public Vector3 position = new Vector3();
    private Vector3 velocity = new Vector3();
    public Vector3 Velocity { get{ return velocity; } }

    public Action<SpringVector3> callback = (pos) => { };

    public override void update(float delta)
    {
        if (state == SpringState.DELETE) {
            return;
        }
        if (target == position)
        {
            return;
        }
        Vector3 force = -tension * (position - target);
        Vector3 damping = -friction * velocity;
        Vector3 acceleration = (force + damping) / mass;
        velocity = velocity + acceleration;
        position = position + velocity * delta;
        callback(this);
    }
}

[System.Serializable]
public class SpringImpulseVector3 : BaseSpring
{

    public Vector3 target = new Vector3();
    public Vector3 position = new Vector3();
    private Vector3 velocity = new Vector3();
    public Vector3 Velocity { get { return velocity; } }
    private Vector3 force = new Vector3();
    public Vector3 Force { get { return force; } }

    public override void update(float delta)
    {
        if (state == SpringState.DELETE)
        {
            return;
        }
        if (target == position)
        {
            return;
        }
        force = Vector3.zero;
        force = -tension * (position - target);
        Vector3 damping = -friction * velocity;
        Vector3 acceleration = (force + damping) / mass;

        velocity = velocity + acceleration;
        position = position + velocity * delta;
        //callback(this);
    }
}