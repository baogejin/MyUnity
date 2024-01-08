using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    private Canvas _canvas;
    private void Awake()
    {
        _canvas = transform.Find("Canvas").GetComponent<Canvas>();
        Game.UIManager.SetCanvas(_canvas);
    }
    void Start()
    {
        Game.AudioManager.PlayBgm("Assets/GameRes/Audio/BGM1");
        Game.UIManager.ShowUI(GameUI.MainUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
