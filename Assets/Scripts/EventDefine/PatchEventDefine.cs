public class PatchEventDefine
{
    /// <summary>
    /// ��������ʼ��ʧ��
    /// </summary>
    public class InitializeFailed : IEventMessage
    {
        public static void SendEventMessage()
        {
            var msg = new InitializeFailed();
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// �������̲���ı�
    /// </summary>
    public class PatchStatesChange : IEventMessage
    {
        public string Tips;

        public static void SendEventMessage(string tips)
        {
            var msg = new PatchStatesChange();
            msg.Tips = tips;
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// ���ָ����ļ�
    /// </summary>
    public class FoundUpdateFiles : IEventMessage
    {
        public int TotalCount;
        public long TotalSizeBytes;

        public static void SendEventMessage(int totalCount, long totalSizeBytes)
        {
            var msg = new FoundUpdateFiles();
            msg.TotalCount = totalCount;
            msg.TotalSizeBytes = totalSizeBytes;
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// ���ؽ��ȸ���
    /// </summary>
    public class DownloadProgressUpdate : IEventMessage
    {
        public int TotalDownloadCount;
        public int CurrentDownloadCount;
        public long TotalDownloadSizeBytes;
        public long CurrentDownloadSizeBytes;

        public static void SendEventMessage(int totalDownloadCount, int currentDownloadCount, long totalDownloadSizeBytes, long currentDownloadSizeBytes)
        {
            var msg = new DownloadProgressUpdate();
            msg.TotalDownloadCount = totalDownloadCount;
            msg.CurrentDownloadCount = currentDownloadCount;
            msg.TotalDownloadSizeBytes = totalDownloadSizeBytes;
            msg.CurrentDownloadSizeBytes = currentDownloadSizeBytes;
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// ��Դ�汾�Ÿ���ʧ��
    /// </summary>
    public class PackageVersionUpdateFailed : IEventMessage
    {
        public static void SendEventMessage()
        {
            var msg = new PackageVersionUpdateFailed();
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// �����嵥����ʧ��
    /// </summary>
    public class PatchManifestUpdateFailed : IEventMessage
    {
        public static void SendEventMessage()
        {
            var msg = new PatchManifestUpdateFailed();
            EventManager.Instance.SendMessage(msg);
        }
    }

    /// <summary>
    /// �����ļ�����ʧ��
    /// </summary>
    public class WebFileDownloadFailed : IEventMessage
    {
        public string FileName;
        public string Error;

        public static void SendEventMessage(string fileName, string error)
        {
            var msg = new WebFileDownloadFailed();
            msg.FileName = fileName;
            msg.Error = error;
            EventManager.Instance.SendMessage(msg);
        }
    }
}