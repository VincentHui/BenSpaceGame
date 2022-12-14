using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class FactoryExtensions
{
    public static void PublishGlobal(this GameObject p_obj, string p_eventName)
	{
		GameFactory.Instance.gameObject.Broker().PublishGameEvent(p_eventName, GameFactory.Instance.gameObject, "NO_MSG_GLOBAL");
	}
    public static void SubscribeGlobal(this GameObject p_obj, string p_eventName, Action p_action)
	{
		GameFactory.Instance.gameObject.Broker().SubscribeGameEvent<string>(p_eventName, (s)=>p_action());
	}	
	public static void PublishGlobal<T>(this GameObject p_obj, string p_eventName, T msg)
	{
		GameFactory.Instance.gameObject.Broker().PublishGameEvent(p_eventName, GameFactory.Instance.gameObject, msg);
	}	
	public static void SubscribeGlobal<T>(this GameObject p_obj, string p_eventName, Action<MessagePayload<T>> p_action)
	{
		GameFactory.Instance.gameObject.Broker().SubscribeGameEvent<T>(p_eventName, p_action);
	}
}   
[RequireComponent(typeof(MessagerBroker))]
public class GameFactory : Singleton<GameFactory> {
	MessagerBroker broker;
    private GameObject player;

    // Use this for initialization
    void Start () {

        gameObject.SubscribeBroker<GameObject>("RESTART", (pay)=>{

        });
	}

}
