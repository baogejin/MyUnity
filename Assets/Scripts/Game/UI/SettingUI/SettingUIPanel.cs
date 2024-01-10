using UnityEngine;
using UnityEngine.UI;

public class SettingUIPanel : BasePanel
{
    public SettingUIPanel()
    {
        view = CreateView("Assets/GameRes/UI/SettingUI");
    }

    private Slider _bgmSlider;
    private Slider _effectSlider;
    private Button _closeBtn;
    public override string GetUIName()
    {
        return GameUI.SettingUI;
    }

    protected override void OnInit()
    {
        _bgmSlider = view.transform.Find("Frame/BgmSlider").GetComponent<Slider>();
        _effectSlider = view.transform.Find("Frame/EffectSlider").GetComponent <Slider>();
        _closeBtn = view.transform.Find("Frame/CloseBtn").GetComponent<Button>();

        _bgmSlider.value = Game.AudioManager.BgmVolume;
        _effectSlider.value = Game.AudioManager.EffectVolume;

        _bgmSlider.onValueChanged.AddListener(OnChangeBgmVolume);
        _effectSlider.onValueChanged.AddListener(OnChangeEffectVolume);
        _closeBtn.onClick.AddListener(ClosePanel);
        Time.timeScale = 0f;
    }

    private void OnChangeBgmVolume(float value)
    {
        Game.AudioManager.BgmVolume = value;
    }

    private void OnChangeEffectVolume(float value)
    {
        Game.AudioManager.EffectVolume = value;
    }

    protected override void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}