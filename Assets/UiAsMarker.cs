using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct MarkerParams {
    public string message;
    public float duration;
}

public static class MarkerExtension {
    public static void SendMarkerMessage(this GameObject obj, string p_message, float p_duration = 3) {
        obj.PublishGlobal("ChangeMarker", new MarkerParams() { message= p_message, duration= p_duration });
    }
}

public class UiAsMarker : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Camera cam;
    private string currentMessage;
    private void Awake()
    {
        var scaleSpring = gameObject.useSpring((s)=> { text.color = new Color(1, 1, 1, s.position); }, 0, 0);
        gameObject.SubscribeGlobal<MarkerParams>("ChangeMarker", (m) => {

            Debug.Log(m.What.message);



            gameObject.AddToSchedule(() =>
            {
                scaleSpring.target = 1f;
                //text.color = new Color(1, 1, 1, scaleSpring.position);
                text.text = m.What.message;
                Debug.Log("START");
            }, (t) => { }, () =>
            {
                //text.color = new Color(1, 1, 1, 0);
                scaleSpring.target = 0f;
                //text.text = "";
                Debug.Log("END");
            }, m.What.duration, "UI_MARKER");
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //text.color = new Color(1, 1, 1, 0);
        //text.text = currentMessage;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 uiPos = new Vector3(screenPos.x, Screen.height - screenPos.y, screenPos.z);

        text.transform.position = uiPos;

    }
}
