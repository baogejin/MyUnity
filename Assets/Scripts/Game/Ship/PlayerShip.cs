using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Transform _ship;
    private Transform _direction;
    public GameObject CollectBullet;

    private PlayerStatus _status = new PlayerStatus();

    private bool _move = false;
    private void Awake()
    {
        _direction = transform.Find("Direction");
        _ship = transform.Find("Ship");

        BodyInfo bodyInfo = GameData.SpaceShipCfg.Get().GetBodyByID(Game.PlayerInfo.shipBody);
        if (bodyInfo != null)
        {
            _status.maxHealth = bodyInfo.MaxHealth;
            _status.health = bodyInfo.MaxHealth;
        }
        BatteryInfo batteryInfo = GameData.SpaceShipCfg.Get().GetBatteryByID(Game.PlayerInfo.battery);
        if (batteryInfo != null)
        {
            _status.maxPower = batteryInfo.MaxPower;
            _status.power = batteryInfo.MaxPower;
        }
        SolarPanelInfo solarPanelInfo = GameData.SpaceShipCfg.Get().GetSolarPanelByID(Game.PlayerInfo.solarPanel);
        if (solarPanelInfo != null)
        {
            _status.addPowerRate = solarPanelInfo.PowerRate;
        }
        ThrusterInfo thrusterInfo = GameData.SpaceShipCfg.Get().GetThrusterByID(Game.PlayerInfo.thruster);
        if (thrusterInfo != null)
        {
            _status.thrusterPowerRate = thrusterInfo.PowerRate;
            _status.thrusterSpeed = thrusterInfo.MaxSpeed;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction.Rotate(0f, 0f, Time.deltaTime * 90f);
        if (_move)
        {
            if (_status.power >= _status.thrusterPowerRate * Time.deltaTime)
            {
                transform.position += _ship.transform.up * Time.deltaTime * _status.thrusterSpeed;
                _status.power -= _status.thrusterPowerRate * Time.deltaTime;
            }
            else
            {
                _move = false;
            }
        }
        _status.power += _status.addPowerRate * Time.deltaTime;
        if (_status.power > _status.maxPower)
        {
            _status.power = _status.maxPower;
        }
        PlayerEventDefine.PlayerStatusUpdate.SendEventMessage(_status);
    }

    void OnMove()
    {
        if (Time.timeScale== 0f)
        {
            return;
        }
        _ship.rotation = _direction.rotation;
        _move = true;
    }

    void OnStop()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        _move = false;
    }

    void OnAction1()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        var go = GameObject.Instantiate(CollectBullet, transform.position, _direction.rotation);
        CollectBullet bullet = go.GetComponent<CollectBullet>();
        if (bullet != null )
        {
            bullet.SetShip(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mine mine = collision.GetComponent<Mine>();
        if (mine != null)
        {
            mine.Collect();
        }
    }
}
