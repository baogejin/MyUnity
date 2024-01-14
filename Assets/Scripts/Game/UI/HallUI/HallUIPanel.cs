using UnityEngine;
using UnityEngine.UI;
using YooAsset;

public class HallUIPanel : BasePanel
{
    public HallUIPanel()
    {
        view = CreateView("Assets/GameRes/UI/HallUI");
    }

    private Button _startBtn;
    private Button _upgradeBtn;
    private Button _assembleBtn;
    private Button _shopBtn;
    private Text _goldNum;
    public override string GetUIName()
    {
        return GameUI.HallUI;
    }

    protected override void OnInit()
    {
        _startBtn = view.transform.Find("StartBtn").GetComponent<Button>();
        _upgradeBtn = view.transform.Find("UpgradeBtn").GetComponent<Button>();
        _assembleBtn = view.transform.Find("AssembleBtn").GetComponent<Button>();
        _shopBtn = view.transform.Find("ShopBtn").GetComponent <Button>();
        _goldNum = view.transform.Find("Gold/GoldNum").GetComponent<Text>();

        _goldNum.text = Game.PlayerInfo.goldNum.ToString();

        _startBtn.onClick.AddListener(OnStart);
        eventGroup.AddListener<PlayerEventDefine.PlayerGoldChange>(OnPlayerGoldChange);
    }

    protected override void OnDestroy()
    {
    }

    private void OnStart()
    {
        Game.BattleInfo.InitBattle();
        Game.UIManager.ShowUI(GameUI.LoadingUI);
        YooAssets.LoadSceneAsync("Assets/GameRes/Scenes/Game");

    }

    void OnPlayerGoldChange(IEventMessage msg)
    {
        int num = (msg as PlayerEventDefine.PlayerGoldChange).num;
        _goldNum.text = num.ToString();
    }
}