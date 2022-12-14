using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using System;

public static class UIEventExtension {
    private enum UIEvent
    {
        SELECT,
        CANCEL,
        DESELECT,
        SUBMIT
    }

    //public static void onUIEvent(this Transform p_obj, UIEvent type, Action callback) {
    //    onUiEvent(p_obj.gameObject, type, callback);
    //}

    private static string enumToString(UIEvent type) {
        switch (type)
        {
            case UIEvent.SELECT:
                return "SELECT";
            case UIEvent.CANCEL:
                return "CANCEL";
            case UIEvent.DESELECT:
                return "DESELECT";
            case UIEvent.SUBMIT:
                return "SUBMIT";
            default:
                return "";
        }
    }
    //private static void onUiEvent(this GameObject p_obj, UIEvent type, Action callback) {

    //    p_obj.SubscribeBroker(enumToString(type), callback);
    //}

    public static UIEventEmitter Emitter(this GameObject p_obj)
    {
        UIEventEmitter emitter = p_obj.GetComponent<UIEventEmitter>();
        return emitter == null ? p_obj.AddComponent<UIEventEmitter>() : emitter;
    }

    public static void onUiSelect(this GameObject p_obj, Action callback)
    {
        p_obj.SubscribeBroker(enumToString(UIEvent.SELECT), callback);
    }

    public static void onUiDeselect(this GameObject p_obj, Action callback)
    {
        p_obj.SubscribeBroker(enumToString(UIEvent.DESELECT), callback);
    }

    public static void onUiSubmit(this GameObject p_obj, Action callback)
    {
        p_obj.SubscribeBroker(enumToString(UIEvent.SUBMIT), callback);
    }

    public static void onUiCancel(this GameObject p_obj, Action callback)
    {
        p_obj.SubscribeBroker(enumToString(UIEvent.CANCEL), callback);
    }


}

public class UIEventEmitter : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, ICancelHandler
{
    public void OnCancel(BaseEventData eventData)
    {
        gameObject.PublishBroker("CANCEL");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.PublishBroker("DESELECT");
    }

    public void OnSelect(BaseEventData eventData)
    {
        gameObject.PublishBroker("SELECT");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        gameObject.PublishBroker("SUBMIT");
    }
}
