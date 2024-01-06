using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YooAsset;

public class EventManager
{
    private class PostWrapper
    {
        public int PostFrame;
        public int EventID;
        public IEventMessage Message;

        public void OnRelease()
        {
            PostFrame = 0;
            EventID = 0;
            Message = null;
        }
    }

    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
                _instance.Initalize();
            }
                
            return _instance;
        }
    }

    private bool _isInitialize = false;
    private GameObject _driver = null;
    private readonly Dictionary<int, LinkedList<Action<IEventMessage>>> _listeners = new Dictionary<int, LinkedList<Action<IEventMessage>>>();
    private readonly List<PostWrapper> _postingList = new List<PostWrapper>();

    /// <summary>
    /// 初始化事件系统
    /// </summary>
    public void Initalize()
    {
        if (_isInitialize)
            throw new Exception($"{nameof(EventManager)} is initialized !");

        if (_isInitialize == false)
        {
            // 创建驱动器
            _isInitialize = true;
            _driver = new UnityEngine.GameObject($"[{nameof(EventManager)}]");
            _driver.AddComponent<EventDriver>();
            UnityEngine.Object.DontDestroyOnLoad(_driver);
            Debug.Log($"{nameof(EventManager)} initalize !");
        }
    }

    /// <summary>
    /// 销毁事件系统
    /// </summary>
    public void Destroy()
    {
        if (_isInitialize)
        {
            ClearAll();

            _isInitialize = false;
            if (_driver != null)
                GameObject.Destroy(_driver);
            Debug.Log($"{nameof(EventManager)} destroy all !");
        }
    }

    /// <summary>
    /// 更新事件系统
    /// </summary>
    internal void Update()
    {
        for (int i = _postingList.Count - 1; i >= 0; i--)
        {
            var wrapper = _postingList[i];
            if (UnityEngine.Time.frameCount > wrapper.PostFrame)
            {
                SendMessage(wrapper.EventID, wrapper.Message);
                _postingList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 清空所有监听
    /// </summary>
    public void ClearAll()
    {
        foreach (int eventId in _listeners.Keys)
        {
            _listeners[eventId].Clear();
        }
        _listeners.Clear();
        _postingList.Clear();
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    public void AddListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : IEventMessage
    {
        System.Type eventType = typeof(TEvent);
        int eventId = eventType.GetHashCode();
        AddListener(eventId, listener);
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    public void AddListener(System.Type eventType, System.Action<IEventMessage> listener)
    {
        int eventId = eventType.GetHashCode();
        AddListener(eventId, listener);
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    public void AddListener(int eventId, System.Action<IEventMessage> listener)
    {
        if (_listeners.ContainsKey(eventId) == false)
            _listeners.Add(eventId, new LinkedList<Action<IEventMessage>>());
        if (_listeners[eventId].Contains(listener) == false)
            _listeners[eventId].AddLast(listener);
    }


    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : IEventMessage
    {
        System.Type eventType = typeof(TEvent);
        int eventId = eventType.GetHashCode();
        RemoveListener(eventId, listener);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener(System.Type eventType, System.Action<IEventMessage> listener)
    {
        int eventId = eventType.GetHashCode();
        RemoveListener(eventId, listener);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener(int eventId, System.Action<IEventMessage> listener)
    {
        if (_listeners.ContainsKey(eventId))
        {
            if (_listeners[eventId].Contains(listener))
                _listeners[eventId].Remove(listener);
        }
    }


    /// <summary>
    /// 实时广播事件
    /// </summary>
    public void SendMessage(IEventMessage message)
    {
        int eventId = message.GetType().GetHashCode();
        SendMessage(eventId, message);
    }

    /// <summary>
    /// 实时广播事件
    /// </summary>
    public void SendMessage(int eventId, IEventMessage message)
    {
        if (_listeners.ContainsKey(eventId) == false)
            return;

        LinkedList<Action<IEventMessage>> listeners = _listeners[eventId];
        if (listeners.Count > 0)
        {
            var currentNode = listeners.Last;
            while (currentNode != null)
            {
                currentNode.Value.Invoke(message);
                currentNode = currentNode.Previous;
            }
        }
    }

    /// <summary>
    /// 延迟广播事件
    /// </summary>
    public void PostMessage(IEventMessage message)
    {
        int eventId = message.GetType().GetHashCode();
        PostMessage(eventId, message);
    }

    /// <summary>
    /// 延迟广播事件
    /// </summary>
    public void PostMessage(int eventId, IEventMessage message)
    {
        var wrapper = new PostWrapper();
        wrapper.PostFrame = UnityEngine.Time.frameCount;
        wrapper.EventID = eventId;
        wrapper.Message = message;
        _postingList.Add(wrapper);
    }
}
