using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private Canvas _canvas;

    private Dictionary<string, BasePanel> _panels = new Dictionary<string, BasePanel>();

    public void SetCanvas(Canvas canvas)
    {
        _canvas = canvas;
        if (_panels != null)
        {
            foreach (var panel in _panels.Values)
            {
                panel.Destroy();
            }
        }
        _panels = new Dictionary<string, BasePanel>();
    }

    public void ShowUI(string name)
    {
        if (_canvas == null)
        {
            return;
        }

        if (_panels.ContainsKey(name))
        {
            return;
        }
        BasePanel panel = CreatePanel(name);
        if (panel == null)
        {
            return;
        }
        panel.SetParent(_canvas.transform);
        panel.Init();
        _panels[name] = panel;
    }

    public void HideUI(string name)
    {
        if (!_panels.ContainsKey(name)) { return; }
        BasePanel panel = _panels[name];
        panel.Hide();
        _panels.Remove(name);
    }

    private BasePanel CreatePanel(string name)
    {
        switch (name)
        {
            case GameUI.MainUI:
                return new MainUIPanel();
            case GameUI.SettingUI:
                return new SettingUIPanel();
        }
        return null;
    }
}

static class GameUI
{
    public const string MainUI = "MainUI";
    public const string SettingUI = "SettingUI";
}