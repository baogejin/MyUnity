using GameData;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

public class Boot : MonoBehaviour
{
    /// <summary>
    /// 资源系统运行模式
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;
    private PatchWindow _patchWindow;

    private void Awake()
    {
        Debug.Log($"资源系统运行模式：{PlayMode}");
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Game.Instance.Init(this);
        // 初始化资源系统
        YooAssets.Initialize();

        // 加载更新页面
        var go = Resources.Load<GameObject>("PatchWindow");
        GameObject.Instantiate(go);

        // 开始补丁更新流程
        PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), PlayMode);
        YooAssets.StartOperation(operation);
        yield return operation;

        // 设置默认的资源包
        var gamePackage = YooAssets.GetPackage("DefaultPackage");
        YooAssets.SetDefaultPackage(gamePackage);

        //定时清理资源包
        InvokeRepeating("UnloadUnusedAssets", 300f, 300f);

        YooAssets.LoadSceneAsync("Assets/GameRes/Scenes/HallScene");
    }

    /// <summary>
    /// 卸载所有引用计数为零的资源包
    /// </summary>
    private void UnloadUnusedAssets()
    {
        Debug.Log("UnloadUnusedAssets");
        var package = YooAssets.GetPackage("DefaultPackage");
        package.UnloadUnusedAssets();
    }

    private void Update()
    {
        SystemEventDefine.EngineUpdate.SendEventMessage();
    }
}

