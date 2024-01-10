using UnityEngine;

public class SystemEventDefine
{
    public class EngineUpdate : IEventMessage
    {
        public static void SendEventMessage()
        {
            var msg = new EngineUpdate();
            EventManager.Instance.SendMessage(msg);
        }
    }
}