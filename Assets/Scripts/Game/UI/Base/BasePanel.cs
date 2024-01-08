public abstract class BasePanel:BaseView
{
    public void Hide()
    {
        view.transform.SetParent(null, false);
        Destroy();
    }

    protected override void OnDestroy()
    {
    }

    protected override void OnInit()
    {
    }

    public abstract string GetUIName();

    protected void ClosePanel()
    {
        Game.UIManager.HideUI(GetUIName());
    }
}