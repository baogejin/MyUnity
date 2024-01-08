using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YooAsset;

public abstract class BaseView
{
    protected GameObject view;
    protected List<AssetHandle> handles = new List<AssetHandle>();
    protected readonly EventGroup eventGroup = new EventGroup();

    protected GameObject CreateView(string path)
    {
        AssetHandle handle = YooAssets.LoadAssetSync<GameObject>(path);
        handles.Add(handle);
        return GameObject.Instantiate(handle.AssetObject as GameObject);
    }

    public void Destroy()
    {
        OnDestroy();
        foreach (AssetHandle handle in handles)
        {
            handle.Release();
        }
        eventGroup.RemoveAllListener();
        GameObject.Destroy(view);
    }

    protected abstract void OnDestroy();

    public void Init()
    {
        OnInit();
    }

    protected abstract void OnInit();

    public void SetParent(Transform transform)
    {
        view.transform.SetParent(transform, false);
    }
}