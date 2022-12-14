using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;

public class UITextTypeWriter : MonoBehaviour
{
    public Text text;
    //public bool playOnAwake = true;
    //public float delayToStart;
    public const float delayBetweenChars = 0.0125f;

    //Update text and start typewriter effect
    public void ChangeText(string textContent, float delay, string eventOver, bool reverse = false, float wait = 0)
    {
        StartCoroutine(PlayText(textContent, delay, eventOver, reverse, wait));
    }

    public float QuadIn(float t) { return t * t; }
    IEnumerator PlayText(string story, float delay, string eventFinish, bool reverse = false, float wait = 0)
    {
        float NewDelay = delay;
        for (int i = 0; i < story.Length; i++)
        {
            text.text = !reverse ? text.text + story[i] : story[i] + text.text;
            yield return new WaitForSeconds(NewDelay);
        }
        yield return new WaitForSeconds(wait);
        gameObject.PublishGlobal(eventFinish);
    }
}