using Unity.VisualScripting;
using UnityEngine;
using YooAsset;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class SpaceObject : MonoBehaviour
{
    protected int Health = 3;
    protected int MaxHealth = 3;
    private AssetHandle _handle;
    private HealthBar _healthBar;

    protected virtual void Awake()
    {
        _handle = YooAssets.LoadAssetSync<GameObject>("Assets/GameRes/SpaceObjects/HealthBar");
        GameObject go = Instantiate(_handle.AssetObject as GameObject,transform);
        go.transform.position = transform.position + new Vector3(0,1,0);
        _healthBar = go.GetComponent<HealthBar>();
        _healthBar.UpdateHealth(Health, MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        _healthBar.UpdateHealth(Health, MaxHealth);
        if (Health <= 0)
        {
            Health = 0;
            OnBroken();
        }
    }

    protected virtual void OnBroken()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        _handle.Release();
    }
}