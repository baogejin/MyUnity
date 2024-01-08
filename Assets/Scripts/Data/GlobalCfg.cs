using GameData;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace GameData
{
    public class GlobalInfo
    {
        public string Key;
        public int Value;
        public List<List<int>> SlcValue;
        public string StrValue;
    }

    public class GlobalCfg
    {
        public List<GlobalInfo> Global;
        private Dictionary<string, GlobalInfo> _GlobalDict;
        private static GlobalCfg _instance;
        public static GlobalCfg Get()
        {
            if (_instance == null)
                Init();
            return _instance;
        }

        private static void Init()
        {
            AssetHandle handle = YooAssets.LoadAssetSync<TextAsset>("Assets/GameRes/Json/Global");
            TextAsset text = handle.AssetObject as TextAsset;
            _instance = JsonConvert.DeserializeObject<GlobalCfg>(text.text);
            handle.Release();
        }

        public GlobalInfo GetInfoByKey(string key)
        {
            if (_GlobalDict == null)
                InitDict();
            if (!_GlobalDict.ContainsKey(key))
                return null;
            return _GlobalDict[key];
        }

        private void InitDict()
        {
            _GlobalDict = new Dictionary<string, GlobalInfo>();
            foreach (GlobalInfo info in Global)
            {
                _GlobalDict.Add(info.Key, info);
            }
        }
    }
}
