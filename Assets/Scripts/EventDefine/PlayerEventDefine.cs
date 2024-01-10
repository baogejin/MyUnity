public class PlayerEventDefine
{
	public class PlayerGoldChange : IEventMessage
	{
		public int num;
		public static void SendEventMessage(int num)
		{
			var msg = new PlayerGoldChange();
			msg.num = num;
			EventManager.Instance.SendMessage(msg);
		}
	}

	public class PlayerStatusUpdate : IEventMessage
	{
		public PlayerStatus status;
        public static void SendEventMessage(PlayerStatus s)
        {
            var msg = new PlayerStatusUpdate();
			msg.status = s;
            EventManager.Instance.SendMessage(msg);
        }
    }
}