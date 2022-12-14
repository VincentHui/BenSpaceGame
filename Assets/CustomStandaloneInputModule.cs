using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomStandaloneInputModule : StandaloneInputModule
{
    public GameObject GetHovered()
    {
        var mouseEvent = GetLastPointerEventData(-1);
        if (mouseEvent == null)
            return null;
        return mouseEvent.pointerCurrentRaycast.gameObject;
    }

    public GameObject GetPointerPress()
    {
        var mouseEvent = GetLastPointerEventData(-1);
        if (mouseEvent == null)
            return null;
        if (mouseEvent.pointerClick == null)
            return null;
        return mouseEvent.pointerClick.gameObject;
    }

    public List<GameObject> GetAllHovered()
    {
        var mouseEvent = GetLastPointerEventData(-1);
        if (mouseEvent == null)
            return null;
        return mouseEvent.hovered;
    }
}