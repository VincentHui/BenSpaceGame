using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChainAnimation : MonoBehaviour
{
    List<Spring> springs;
    private IEnumerator chainAnimation(List<Spring> items,float waitTime, float target)
    {
        
        foreach (var item in items) {
            Debug.Log(target);
            yield return new WaitForSeconds(waitTime);
            item.target = target;

        }
    }
    private List<Spring> getSprings(List<Transform> items) {
        //List<Spring> result = new List<Spring>();
        //foreach (var item in items)
        //{
        //    Spring scaleSpring = gameObject.useSpring(pos => item.position = new Vector3(pos.position, item.position.y, item.position.z));
        //    scaleSpring.tension = 5;
        //    scaleSpring.position = (Screen.width + item.GetComponent<RectTransform>().rect.width);
        //    scaleSpring.target = (Screen.width + item.GetComponent<RectTransform>().rect.width);
        //    result.Add(scaleSpring);
        //}

        return gameObject.useSprings(items, (s, item) => {
            item.position = new Vector3(s.position, item.position.y, item.position.z);
            s.tension = 5;
        }, (s,item)=> { })._Springs;
    }

    void Awake()
    {
  
        gameObject.SubscribeBroker<List<Transform>>("START_GROUP", (payload) => {
            //Debug.Log("START ANIM!");
            springs = getSprings(payload.What);
            StartCoroutine(chainAnimation(springs, 0.1f, Screen.width / 2));
        });

        gameObject.SubscribeBroker("CLOSE_GROUP", () => {
            StartCoroutine(chainAnimation(springs, 0.1f, 0 - Screen.width / 2));
        });
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


