using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GridBrushBase;
using static UnityEngine.ParticleSystem;

public static class poolExtensions {
    public static ObjectPool<GameObject> MakePool(this GameObject toMake, int poolAmount) {
        return new ObjectPool<GameObject>(() => {
            //return
            var made = GameObject.Instantiate(toMake);
            made.SetActive(false);
            return made;

        }, poolAmount);
    }
}
[System.Serializable]
public class ObjectPool<T>
{
    public List<T> Active = new List<T>();
    public List<T> Inactive = new List<T>();
    public bool CanGetFromPool { get { return Inactive.Count > 0; } }

    public T GetFromPool()
    {
        return moveBetweenLists(Inactive, Active); ;
    }

    public void ReturnToPool(T toReturn)
    {
        moveBetweenLists(Active, Inactive, toReturn);
    }

    public void ReturnToPool(int index)
    {
        moveBetweenLists(Active, Inactive, index);
    }

    public static T  moveBetweenLists<T>(List<T> from, List<T> to, int index =0)
    {
        var selected = from[index];
        from.RemoveAt(index);
        to.Add(selected);
        return selected;
    }

    public static T moveBetweenLists<T>(List<T> from, List<T> to, T toRemove)
    {
        from.Remove(toRemove);
        to.Add(toRemove);
        return toRemove;
    }

    public ObjectPool(Func<T> construct, int poolAmount = 10)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            Inactive.Add(construct());
        }
    }
}

[System.Serializable]
public class ManualParticle: IEqualityComparer<ManualParticle>
{
    public float TimeToLive;
    public GameObject sprite;

    public bool Equals(ManualParticle x, ManualParticle y)
    {
        return x.sprite == y.sprite;
    }

    public int GetHashCode(ManualParticle obj)
    {
        return obj.sprite.GetHashCode();
    }
}

[System.Serializable]
public class Bullet : IEqualityComparer<Bullet>
{
    public GameObject bulletObject;

    public bool Equals(Bullet x, Bullet y)
    {
        return x.bulletObject == y.bulletObject;
    }

    public int GetHashCode(Bullet obj)
    {
        return obj.bulletObject.GetHashCode();
    }
}

public class ParticleBulletPoolBase : MonoBehaviour {
    public GameObject toSpawn;
    public ObjectPool<Bullet> pool;
    protected void MakePool(int amount)
    {
        pool = new ObjectPool<Bullet>(() => {
            toSpawn = Instantiate(toSpawn);
            toSpawn.SetActive(false);
            var particle = new Bullet()
            {
                bulletObject = toSpawn,
            };
            return particle;
        }, amount);
    }
    protected Bullet Spawn()
    {
        var selected = pool.GetFromPool();
        selected.bulletObject.SetActive(true);
        selected.bulletObject.transform.position = transform.position;
        return selected;
    }

    public void ReturnToPool(Bullet toReturn) {
        pool.ReturnToPool(toReturn);
    }

}

public class ManualParticleSystemBase : MonoBehaviour {
    public GameObject toSpawn;
    public ObjectPool<ManualParticle> pool;

    protected void MakePool(int amount) {

        pool = new ObjectPool<ManualParticle>(() => {
            toSpawn = Instantiate(toSpawn);
            toSpawn.SetActive(false);
            var particle = new ManualParticle()
            {
                sprite = toSpawn,
                TimeToLive = 0f
            };
            return particle;
        }, amount);
    }

    protected ManualParticle Spawn(float timeToLive)
    {
        var selected = pool.GetFromPool();
        selected.sprite.SetActive(true);
        selected.sprite.transform.position = transform.position;
        selected.TimeToLive = timeToLive;
        return selected;
    }

    protected void UpdateParticlesTimeToLive(float deltaTime)
    {
        //particles.ForEach((particle) => {
        for (int i = pool.Active.Count - 1; i >= 0; i--)
        {
            pool.Active[i].TimeToLive -= deltaTime;
            if (pool.Active[i].TimeToLive <= 0)
            {
                pool.Active[i].TimeToLive = 0;
                pool.Active[i].sprite.SetActive(false);
                pool.ReturnToPool(i);
            }
        }
    }

    void LateUpdate() {
        UpdateParticlesTimeToLive(Time.deltaTime);
    }
}

public class ManualParticleSystem : ManualParticleSystemBase
{
    // Start is called before the first frame update
    void Start()
    {
        MakePool(40);
    }
    float time = 0f;
    public float spawnDelay = 2f;
    public float timeToLive = 2f;

    // Update is called once per frame
    void Update()
    {
        if (!pool.CanGetFromPool)
        {
            return;
        }
        if (time >= spawnDelay)
        {
            Spawn(timeToLive);
            time = 0f;
        }
        time += Time.deltaTime;
        //UpdateParticlesTimeToLive(Time.deltaTime);
    }
}
