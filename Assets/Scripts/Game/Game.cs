using System.Collections;
using UnityEngine;
using YooAsset;

public class Game
{
    private static Game _instance;

    /// <summary>
    /// 获取单例
    /// </summary>
    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }
    }

    private AudioManager _audioManager;
    public static AudioManager AudioManager
    {
        get
        {
            return _instance._audioManager;
        }
    }

    private UIManager _uiManager;
    public static UIManager UIManager
    {
        get
        {
            return _instance._uiManager;
        }
    }

    private PlayerInfo _playerInfo;
    public static PlayerInfo PlayerInfo
    {
        get
        {
            return _instance._playerInfo;
        }
    }

    private BattleInfo _battleInfo;
    public static BattleInfo BattleInfo
    {
        get
        {
            return _instance._battleInfo;
        }
    }

    public void Init(MonoBehaviour monoBehaviour)
    {
        _behaviour = monoBehaviour;
        AudioSource audioSource = _behaviour.gameObject.transform.Find("Audio").GetComponent<AudioSource>();
        _audioManager = new AudioManager(audioSource);
        _uiManager = new UIManager();
        _playerInfo = new PlayerInfo();
        _playerInfo.Init();
        _battleInfo = new BattleInfo();
    }

    /// <summary>
    /// 协程启动器
    /// </summary>
    private MonoBehaviour _behaviour;

    /// <summary>
    /// 开启一个协程
    /// </summary>
    public static void StartCoroutine(IEnumerator enumerator)
    {
        if (_instance != null && _instance._behaviour != null)
        {
            _instance._behaviour.StartCoroutine(enumerator);
        }
        else
        {
            Debug.LogError("Game not init");
        }
    }
}
