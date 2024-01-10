using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace GameData
{
    public class BodyInfo
    {
        /// <summary>
        /// 船体ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 最大血量
        /// </summary>
        public int MaxHealth;
        /// <summary>
        /// 升到这一级的费用
        /// </summary>
        public int UpgradeCost;
    }

    public class BatteryInfo
    {
        /// <summary>
        /// 电池ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 最大电力储量
        /// </summary>
        public int MaxPower;
        /// <summary>
        /// 解锁费用
        /// </summary>
        public int Cost;
    }

    public class SolarPanelInfo
    {
        /// <summary>
        /// 太阳能板ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 每秒产生电力
        /// </summary>
        public int PowerRate;
        /// <summary>
        /// 解锁费用
        /// </summary>
        public int Cost;
    }

    public class CollecterLauncherInfo
    {
        /// <summary>
        /// 收集器发射器id
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 发射轨道数量
        /// </summary>
        public int TrackCount;
        /// <summary>
        /// 单轨道cd
        /// </summary>
        public float TrackCD;
        /// <summary>
        /// 解锁花费
        /// </summary>
        public int Cost;
    }

    public class MissileLauncherInfo
    {
        /// <summary>
        /// 导弹发射器id
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 发射轨道数量
        /// </summary>
        public int TrackCount;
        /// <summary>
        /// 单轨道cd
        /// </summary>
        public float TrackCD;
        /// <summary>
        /// 解锁花费
        /// </summary>
        public int Cost;
    }

    public class LaserLauncherInfo
    {
        /// <summary>
        /// 激光发射器id
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 发射电力消耗
        /// </summary>
        public int PowerCost;
        /// <summary>
        /// 激光伤害
        /// </summary>
        public int Damage;
        /// <summary>
        /// 解锁花费
        /// </summary>
        public int Cost;
    }

    public class ThrusterInfo
    {
        /// <summary>
        /// 推进器id
        /// </summary>
        public int ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 每秒消耗电力
        /// </summary>
        public int PowerRate;
        /// <summary>
        /// 最快每秒速度
        /// </summary>
        public float MaxSpeed;
        /// <summary>
        /// 解锁消耗
        /// </summary>
        public int Cost;
    }

    public class SpaceShipCfg
    {
        private static SpaceShipCfg _instance;
        public static SpaceShipCfg Get()
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        public List<BodyInfo> Body;
        private Dictionary<int, BodyInfo> _BodyDict;
        public List<BatteryInfo> Battery;
        private Dictionary<int, BatteryInfo> _BatteryDict;
        public List<SolarPanelInfo> SolarPanel;
        private Dictionary<int, SolarPanelInfo> _SolarPanelDict;
        public List<CollecterLauncherInfo> CollecterLauncher;
        private Dictionary<int, CollecterLauncherInfo> _CollecterLauncherDict;
        public List<MissileLauncherInfo> MissileLauncher;
        private Dictionary<int, MissileLauncherInfo> _MissileLauncherDict;
        public List<LaserLauncherInfo> LaserLauncher;
        private Dictionary<int, LaserLauncherInfo> _LaserLauncherDict;
        public List<ThrusterInfo> Thruster;
        private Dictionary<int, ThrusterInfo> _ThrusterDict;
        private static void Init()
        {
            AssetHandle handle = YooAssets.LoadAssetSync<TextAsset>("Assets/GameRes/Json/SpaceShip");
            TextAsset text = handle.AssetObject as TextAsset;
            _instance = JsonConvert.DeserializeObject<SpaceShipCfg>(text.text);
            handle.Release();
        }

        private void InitDict()
        {
            _BodyDict = new Dictionary<int, BodyInfo>();
            foreach(BodyInfo info in Body)
            {
                _BodyDict.Add(info.ID, info);
            }
            _BatteryDict = new Dictionary<int, BatteryInfo>();
            foreach(BatteryInfo info in Battery)
            {
                _BatteryDict.Add(info.ID, info);
            }
            _SolarPanelDict = new Dictionary<int, SolarPanelInfo>();
            foreach(SolarPanelInfo info in SolarPanel)
            {
                _SolarPanelDict.Add(info.ID, info);
            }
            _CollecterLauncherDict = new Dictionary<int, CollecterLauncherInfo>();
            foreach(CollecterLauncherInfo info in CollecterLauncher)
            {
                _CollecterLauncherDict.Add(info.ID, info);
            }
            _MissileLauncherDict = new Dictionary<int, MissileLauncherInfo>();
            foreach(MissileLauncherInfo info in MissileLauncher)
            {
                _MissileLauncherDict.Add(info.ID, info);
            }
            _LaserLauncherDict = new Dictionary<int, LaserLauncherInfo>();
            foreach(LaserLauncherInfo info in LaserLauncher)
            {
                _LaserLauncherDict.Add(info.ID, info);
            }
            _ThrusterDict = new Dictionary<int, ThrusterInfo>();
            foreach(ThrusterInfo info in Thruster)
            {
                _ThrusterDict.Add(info.ID, info);
            }
        }

        public BodyInfo GetBodyByID(int id)
        {
            if (_BodyDict == null)
            {
                InitDict();
            }
            if (!_BodyDict.ContainsKey(id))
            {
                return null;
            }
            return _BodyDict[id];
        }
        public BatteryInfo GetBatteryByID(int id)
        {
            if (_BatteryDict == null)
            {
                InitDict();
            }
            if (!_BatteryDict.ContainsKey(id))
            {
                return null;
            }
            return _BatteryDict[id];
        }
        public SolarPanelInfo GetSolarPanelByID(int id)
        {
            if (_SolarPanelDict == null)
            {
                InitDict();
            }
            if (!_SolarPanelDict.ContainsKey(id))
            {
                return null;
            }
            return _SolarPanelDict[id];
        }
        public CollecterLauncherInfo GetCollecterLauncherByID(int id)
        {
            if (_CollecterLauncherDict == null)
            {
                InitDict();
            }
            if (!_CollecterLauncherDict.ContainsKey(id))
            {
                return null;
            }
            return _CollecterLauncherDict[id];
        }
        public MissileLauncherInfo GetMissileLauncherByID(int id)
        {
            if (_MissileLauncherDict == null)
            {
                InitDict();
            }
            if (!_MissileLauncherDict.ContainsKey(id))
            {
                return null;
            }
            return _MissileLauncherDict[id];
        }
        public LaserLauncherInfo GetLaserLauncherByID(int id)
        {
            if (_LaserLauncherDict == null)
            {
                InitDict();
            }
            if (!_LaserLauncherDict.ContainsKey(id))
            {
                return null;
            }
            return _LaserLauncherDict[id];
        }
        public ThrusterInfo GetThrusterByID(int id)
        {
            if (_ThrusterDict == null)
            {
                InitDict();
            }
            if (!_ThrusterDict.ContainsKey(id))
            {
                return null;
            }
            return _ThrusterDict[id];
        }
    }
}
