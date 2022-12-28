using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum pointerState {
    Enter,
    Exit,
    Hover,
    PointerDown,
    None
}

public class pointerHandler : MonoBehaviour
{
    //CustomStandaloneInputModule inputmodule;
    public pointerState state = pointerState.None;
    public void Start()
    {
        //inputmodule = EventSystem.current.GetComponent<CustomStandaloneInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
        //var hovered = inputmodule.GetHovered() == gameObject;
        //if (!hovered) {

        //    if (state != pointerState.None) gameObject.PublishBroker("EXIT");
        //    state = pointerState.None;
        //    return;
        //}
        //if (inputmodule.GetPointerPress())
        //{
        //    //if (state != pointerState.Hover) gameObject.PublishBroker("ENTER");
        //    state = pointerState.PointerDown;
        //    gameObject.PublishBroker("POINTER_DOWN");
        //    return;
        //}
        //if (hovered)
        //{
        //    if (state != pointerState.Hover) gameObject.PublishBroker("ENTER");
        //    state = pointerState.Hover;
        //    gameObject.PublishBroker("HOVER");
        //    return;
        //}



    }
}
