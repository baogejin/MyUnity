using System.Collections;
using UnityEngine;

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
                _instance.Init();
            }
            return _instance;
        }
    }

    private void Init()
    {

    }

    /// <summary>
    /// 协程启动器
    /// </summary>
    public MonoBehaviour Behaviour;

    /// <summary>
    /// 开启一个协程
    /// </summary>
    public void StartCoroutine(IEnumerator enumerator)
    {
        if (Behaviour != null)
        {
            Behaviour.StartCoroutine(enumerator);
        }else
        {
            Debug.LogError("Game Behaviour is null");
        }
    }

}
