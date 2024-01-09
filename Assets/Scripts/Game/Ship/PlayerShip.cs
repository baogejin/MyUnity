using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Transform _ship;
    private Transform _direction;
    public GameObject CollectBullet;

    private bool _move = false;
    private void Awake()
    {
        _direction = transform.Find("Direction");
        _ship = transform.Find("Ship");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        _direction.Rotate(0f, 0f, Time.deltaTime * 90f);
        if (_move )
        {
            transform.position += _ship.transform.up * Time.deltaTime * 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
