using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerShip : BaseShip
{
    private Transform _ship;
    private Transform _direction;
    public GameObject collecter;
    public GameObject missile;
    public GameObject laser;

    private PlayerStatus _status = new PlayerStatus();

    private bool _move = false;
    protected override void Awake()
    {
        base.Awake();
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
        _direction.Rotate(0f, 0f, Time.deltaTime * 30f);
        if (_move)
        {
            if (_status.power >= _status.thrusterPowerRate * Time.deltaTime)
            {
                transform.position += _ship.transform.up * Time.deltaTime * _status.thrusterSpeed;
                _status.power -= _status.thrusterPowerRate * Time.deltaTime;
            }
        }
        _status.power += _status.addPowerRate * Time.deltaTime;
        if (_status.power > _status.maxPower)
        {
            _status.power = _status.maxPower;
        }
        PlayerEventDefine.PlayerStatusUpdate.SendEventMessage(_status);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceResource mine = collision.GetComponent<SpaceResource>();
        if (mine != null)
        {
            mine.Collect();
        }
    }

    void OnMove(InputValue value)
    {
        if (value.isPressed)
        {
            if (!_move)
            {
                _ship.rotation = _direction.rotation;
                _move = true;
            }
        }
        else
        {
            _move = false;
        }
    }

    void OnCollecterLaunch()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        var go = GameObject.Instantiate(collecter, transform.position + _direction.up * 2, _direction.rotation);
        ResourceCollecter bullet = go.GetComponent<ResourceCollecter>();
        if (bullet != null)
        {
            bullet.SetShip(gameObject);
        }
    }

    void OnMissileLaunch()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        GameObject.Instantiate(missile, transform.position + _direction.up * 2, _direction.rotation);
    }

    void OnLaserLaunch()
    {
        var go = GameObject.Instantiate(laser, transform.position + _direction.up * 2, _direction.rotation);
        SpriteRenderer s = go.GetComponent<SpriteRenderer>();
        s.size = new Vector2(0.64f, 100f);

        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position+ _direction.up * 52f, new Vector2(0.64f, 100f), _direction.rotation.eulerAngles.z);
        if (collisions.Length > 0)
        {
            foreach(Collider2D collision in collisions)
            {
                SpaceObject spaceObject = collision.GetComponent<SpaceObject>();
                if (spaceObject != null)
                {
                    spaceObject.TakeDamage(1);
                }
            }
        }       
    }
}
