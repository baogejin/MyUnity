using System.Collections;
using UnityEngine;

public class Game
{
    private static Game _instance;

    /// <summary>
    /// ��ȡ����
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
    /// Э��������
    /// </summary>
    public MonoBehaviour Behaviour;

    /// <summary>
    /// ����һ��Э��
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
