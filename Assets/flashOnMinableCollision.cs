using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public static class Memoizer
{
    public static Func<R> Memoize<R>(Func<R> func)
    {
        object cache = null;
        return () =>
        {
            if (cache == null)
                cache = func();
            return (R)cache;
        };
    }

    public static Func<A, R> Memoize<A, R>(Func<A, R> func)
    {
        var cache = new Dictionary<A, R>();
        return a =>
        {
            if (cache.TryGetValue(a, out R value))
                return value;
            value = func(a);
            cache.Add(a, value);
            return value;
        };
    }

    public static Func<A, R> ThreadSafeMemoize<A, R>(Func<A, R> func)
    {
        var cache = new ConcurrentDictionary<A, R>();
        return argument => cache.GetOrAdd(argument, func);
    }
}

public static class MemoizerExtensions
{
    public static Func<R> Memoize<R>(this Func<R> func)
    {
        return Memoizer.Memoize(func);
    }

    public static Func<A, R> Memoize<A, R>(this Func<A, R> func)
    {
        return Memoizer.Memoize(func);
    }

    public static Func<A, R> ThreadSafeMemoize<A, R>(this Func<A, R> func)
    {
        return Memoizer.ThreadSafeMemoize(func);
    }
}

public class flashOnMinableCollision : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] previousMaterials;
    public Material flashMaterial;
    Boolean Flash = false;
    float timeElapsed = 0;
    float flashDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        previousMaterials = meshRenderer.materials;
        gameObject.SubscribeBroker<MinableCollisionInfo>("MINABLE_COLLISION", (obj) => {
            Flash = true;
            timeElapsed = 0;
            meshRenderer.materials = new Material[] { flashMaterial };
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Flash && timeElapsed < flashDuration) {
            timeElapsed += Time.deltaTime;
            return;
        }
        if (Flash && timeElapsed > flashDuration) {
            Flash = false;
            timeElapsed = 0;
            meshRenderer.materials = previousMaterials;
            return;
        }
    }
}
