using System.Collections;
using UnityEngine;
using YooAsset;
using GameData;

public class Boot : MonoBehaviour
{
    /// <summary>
    /// ��Դϵͳ����ģʽ
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

    private void Awake()
    {
        Debug.Log($"��Դϵͳ����ģʽ��{PlayMode}");
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Game.Instance.Behaviour = this;
        // ��ʼ����Դϵͳ
        YooAssets.Initialize();

        // ���ظ���ҳ��
        var go = Resources.Load<GameObject>("PatchWindow");
        GameObject.Instantiate(go);

        // ��ʼ������������
        PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), PlayMode);
        YooAssets.StartOperation(operation);
        yield return operation;

        // ����Ĭ�ϵ���Դ��
        var gamePackage = YooAssets.GetPackage("DefaultPackage");
        YooAssets.SetDefaultPackage(gamePackage);

        Debug.Log(GlobalCfg.Get().GetInfoByKey(GlobalKey.AccountMaxLen).Value);
    }
}

