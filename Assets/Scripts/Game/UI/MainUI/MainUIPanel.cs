using UnityEngine;
using UnityEngine.UI;

public class MainUIPanel : BasePanel
{
    public MainUIPanel()
    {
        view = CreateView("Assets/GameRes/UI/MainUI");
    }

    private Button _settingBtn;

    protected override void OnInit()
    {
        _settingBtn = view.transform.Find("SettingBtn").GetComponent<Button>();

        _settingBtn.onClick.AddListener(OnClickSettingBtn);
    }

    private void OnClickSettingBtn()
    {
        Game.UIManager.ShowUI(GameUI.SettingUI);
    }

    public override string GetUIName()
    {
        return GameUI.MainUI;
    }
}