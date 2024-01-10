public class LoadingUIPanel : BasePanel
{
    public LoadingUIPanel()
    {
        view = CreateView("Assets/GameRes/UI/LoadingUI");
    }
    public override string GetUIName()
    {
        return GameUI.LoadingUI;
    }
}