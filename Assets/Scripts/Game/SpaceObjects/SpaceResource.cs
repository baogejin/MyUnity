public class SpaceResource : SpaceObject
{
    protected override void Awake()
    {
        base.Awake();
        //在battle中注册战斗资源
        Game.BattleInfo.RegisterSpaceRes(gameObject.GetInstanceID());
    }

    public void Collect()
    {
        //在battle中移除战斗资源
        Game.BattleInfo.ClearSpaceRes(gameObject.GetInstanceID());
        Destroy(gameObject);
    }

    protected override void OnBroken()
    {
        Game.BattleInfo.ClearSpaceRes(gameObject.GetInstanceID());
        Destroy(gameObject);
    }
}