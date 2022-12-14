using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PooledObjState {
//    INACTIVE,
//    ACTIVE
//};
[System.Serializable]
public struct pooledObj {
    public GameObject obj;
    //public PooledObjState state;
}



public class SpriteSpawner : MonoBehaviour
{
    public GameObject sprite;
    public List<GameObject> ActiveSpritePool = new List<GameObject>();
    public List<GameObject> InactiveSpritePool = new List<GameObject>();
    public float spawnDelay = 2f;

    public T moveBetweenLists<T>(List<T>from, List<T> to) {
        var selected = from[0];
        from.RemoveAt(0);
        to.Add(selected);
        return selected;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< 5; i++) {
            var obj = Instantiate(sprite);
            obj.SetActive(false);
            InactiveSpritePool.Add(obj);
            //obj.transform.parent = gameObject.transform;
        }
    }
    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (InactiveSpritePool.Count <= 0)
        {
            return;
        }
        if (time >= spawnDelay) {

            var selected = moveBetweenLists(InactiveSpritePool, ActiveSpritePool);
            selected.SetActive(true);
            selected.transform.position = transform.position;
            time = 0f;
        }
    }
}
