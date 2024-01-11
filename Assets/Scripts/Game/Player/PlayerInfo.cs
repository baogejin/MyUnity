using UnityEngine;

public class PlayerInfo
{
    public int goldNum;
    public int shipBody = 1;
    public int battery = 1;
    public int solarPanel = 1;
    public int collecterLauncher = 1;
    public int missileLauncher = 1;
    public int laserLauncher = 1;
    public int thruster = 1;

    public void Init()
    {
        goldNum = PlayerPrefs.GetInt("Gold");
    }

    public void AddGold(int num)
    {
        goldNum += num;
        PlayerPrefs.SetInt("Gold", goldNum);
        PlayerEventDefine.PlayerGoldChange.SendEventMessage(goldNum);
    }
}
