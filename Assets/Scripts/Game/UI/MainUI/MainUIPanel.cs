using UnityEngine;
using UnityEngine.UI;

public class MainUIPanel : BasePanel
{
    private Slider _healthSlider;
    private Text _healthText;
    private Slider _powerSlider;
    private Text _powerText;

    public MainUIPanel()
    {
        view = CreateView("Assets/GameRes/UI/MainUI");
    }

    private Button _settingBtn;

    protected override void OnInit()
    {
        _settingBtn = view.transform.Find("SettingBtn").GetComponent<Button>();
        _healthSlider = view.transform.Find("Health").GetComponent<Slider>();
        _healthText = _healthSlider.transform.Find("HealthText").GetComponent<Text>();
        _powerSlider = view.transform.Find("Power").GetComponent<Slider>();
        _powerText = _powerSlider.transform.Find("PowerText").GetComponent<Text>();

        _settingBtn.onClick.AddListener(OnClickSettingBtn);
        eventGroup.AddListener<PlayerEventDefine.PlayerStatusUpdate>(OnPlayerStatusUpdate);
    }

    private void OnClickSettingBtn()
    {
        Game.UIManager.ShowUI(GameUI.SettingUI);
    }

    public override string GetUIName()
    {
        return GameUI.MainUI;
    }

    private void OnPlayerStatusUpdate(IEventMessage msg)
    {
        PlayerStatus status = (msg as PlayerEventDefine.PlayerStatusUpdate).status;
        _healthSlider.value = status.health / status.maxHealth;
        _healthText.text = Mathf.Floor( status.health).ToString() + "/" + status.maxHealth.ToString();
        _powerSlider.value = status.power / status.maxPower;
        _powerText.text = Mathf.Floor(status.power).ToString() + "/" + status.maxPower.ToString();
    }
}