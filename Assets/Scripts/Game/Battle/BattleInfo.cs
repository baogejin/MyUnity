using System.Collections.Generic;
using UnityEngine;

public class BattleInfo //单场战斗信息
{
    private Dictionary<int, bool> _spaceRes;
    private Dictionary<int, bool> _spaceEnemy;

    public void InitBattle()
    {
        _spaceRes = new Dictionary<int, bool>();
        _spaceEnemy = new Dictionary<int, bool>();
    }

    public void RegisterSpaceRes(int instanceId)
    {
        _spaceRes[instanceId] = true;
        Debug.Log("res count "+_spaceRes.Count);
    }

    public void ClearSpaceRes(int instanceId)
    {
        _spaceRes.Remove(instanceId);
        Debug.Log("res count " + _spaceRes.Count);
        CheckBattleEnd();
    }

    public void RegisterSpaceEnemy(int instanceId)
    {
        _spaceEnemy[instanceId] = true;
    }

    public void ClearSpaceEnemy(int instanceId)
    {
        _spaceEnemy.Remove(instanceId);
        CheckBattleEnd();
    }

    private void CheckBattleEnd()
    {
        if (_spaceRes == null || _spaceEnemy == null)
        {
            return;
        }
        if (_spaceRes.Count == 0 && _spaceEnemy.Count == 0)
        {
            //todo end
            Debug.Log("battle end");
        }
    }
}