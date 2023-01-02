using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct AlphaSlot { public AlphaType type; public GameObject obj; }
public enum AlphaType {
    TEXT_MESH,
    UI_TEXT_MESH,
    UI_RAW_IMAGE,
}
public class alphaOnSelect : MonoBehaviour
{
    List<Spring> alpha = new List<Spring>();
    GameObject selector;

    //public TextMesh text;
    public List<AlphaSlot> alphaToChange = new List<AlphaSlot>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var toChange in alphaToChange) {
            alpha.Add(gameObject.useSpring((s) =>
            {
                if (toChange.type == AlphaType.TEXT_MESH)
                {
                    var text = toChange.obj.GetComponent<TextMesh>();
                    text.color = new Color(text.color.r, text.color.g, text.color.b, s.position);
                }
                if (toChange.type == AlphaType.UI_RAW_IMAGE)
                {
                    var image = toChange.obj.GetComponent<RawImage>();
                    image.color = new Color(image.color.r, image.color.g, image.color.b, s.position);
                }
            }, 0, 0));
        }

        //scale.mass = 1;
        //scale.tension = 3;
        gameObject.SubscribeSelect( (sender) => {
            //alpha.target = 1;
            foreach (var spring in alpha) {
                spring.target = 1f;
                spring.tension = 2f;
            }
            selector = sender.What;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!selector) return;
        if (Vector3.Distance(selector.transform.position, transform.position) > getSelectables.selectionRadius)
        {
            //alpha.target = 0f;
            foreach (var spring in alpha)
            {
                spring.target = 0f;
            }
            selector = null;
        }

    }
}
