using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class MyExtensions
{
	public static MessagerBroker Broker(this GameObject p_obj)
	{
        MessagerBroker broker = p_obj.GetComponent<MessagerBroker>();
        return broker==null ? p_obj.AddComponent<MessagerBroker>() : broker;
	}
    public static void PublishBroker(this GameObject p_obj, string p_eventName)
	{
		p_obj.Broker().PublishGameEvent(p_eventName, p_obj, "NO_MSG");
	}	
    public static void SubscribeBroker(this GameObject p_obj, string p_eventName, Action p_action)
	{
        p_obj.Broker().SubscribeGameEvent<string>(p_eventName, (s) => p_action());
	}	
	public static void PublishBroker<T>(this GameObject p_obj, string p_eventName, T msg)
	{
		p_obj.Broker().PublishGameEvent(p_eventName, p_obj, msg);
	}	
	public static void SubscribeBroker<T>(this GameObject p_obj, string p_eventName, Action<MessagePayload<T>> p_action)
	{
		p_obj.Broker().SubscribeGameEvent<T>(p_eventName, p_action);
	}
    public static bool CheckBroker(GameObject p_ob) {
        return p_ob.GetComponent<MessagerBroker>() != null;
    }
}   
public class MessagerBroker : MonoBehaviour {


// private readonly Dictionary<string, List<GameDirectorEventHandler>> gameEventSubscribers = new Dictionary<string, List<GameDirectorEventHandler>>();
    private MessageBrokerImpl broker = new MessageBrokerImpl();

	public void PublishGameEvent<T>(string p_eventName, GameObject p_caller, T message)
	{
		broker.Publish(p_caller,p_eventName,message);
	}
	public void SubscribeGameEvent<T>(string p_eventName, Action<MessagePayload<T>> subscription)
	{
		broker.Subscribe(p_eventName, subscription);
	}

	public void UnsubscribeGameEvent<T>(string p_eventName, Action<MessagePayload<T>> subscription)
	{
		broker.Unsubscribe(p_eventName, subscription);
	}

    public Dictionary<string, List<Delegate>> subscribers
    {
        get{
            return broker._subscribers;
        }
    }
    public void Clear()
	{
		broker.Clear();
	}
}
    public class MessagePayload<T>
    {
        public GameObject Who { get; private set; }
        public T What { get; private set; }
        // public DateTime When { get; private set; }
        public MessagePayload(T payload, GameObject source)
        {
            Who = source; What = payload;
        }
    }
    // using System.Threading.Tasks;
    public class MessageBrokerImpl
    {
        public readonly Dictionary<string, List<Delegate>> _subscribers;

        public MessageBrokerImpl()
        {
            _subscribers = new Dictionary<string, List<Delegate>>();
        }

        public void Publish<T>( GameObject source,string name, T message)
        {
            if (message == null || source == null)
            {				
                Debug.LogError("this shit null");
                return;
            }
            if(!_subscribers.ContainsKey(name))
            {
				Debug.Log("NO_LISTENERS_FOR {  "+name+"  }");
                return;
            }
			//Debug.Log(name);
            var delegates = _subscribers[name];
            if (delegates == null || delegates.Count == 0) return;
            var payload = new MessagePayload<T>(message, source);
            foreach(var handler in delegates.Select(item => item as Action<MessagePayload<T>>))
            {
				if(handler != null) handler.Invoke(payload);
            }
        }

        public void Subscribe<T>(string name, Action<MessagePayload<T>> subscription)
        {
            var delegates = _subscribers.ContainsKey(name) ? 
                            _subscribers[name] : new List<Delegate>();
            if(!delegates.Contains(subscription))
            {
                delegates.Add(subscription);
            }
            _subscribers[name] = delegates;
        }

        public void Unsubscribe<T>(string name, Action<MessagePayload<T>> subscription)
        {
            if (!_subscribers.ContainsKey(name)) return;
            var delegates = _subscribers[name];
            if (delegates.Contains(subscription))
                delegates.Remove(subscription);
            if (delegates.Count == 0)
                _subscribers.Remove(name);
        }
		public void Clear()
		{
			_subscribers.Clear();
		}
        public void Dispose()
        {
            if(_subscribers!=null) _subscribers.Clear();
        }
    }