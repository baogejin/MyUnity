using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private float _leftTime = 0.5f;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        _leftTime -= Time.deltaTime;
        if (_leftTime < 0)
        {
            Destroy(gameObject);
            return;
        }
        if (_leftTime < 0.3f)
        {
            _sprite.color = new Color(1, 1, 1, _sprite.color.a - Time.deltaTime * 3f);
        }
        
    }
}
