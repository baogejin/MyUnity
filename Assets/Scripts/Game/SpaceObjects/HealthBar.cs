using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private float _targetValue;
    private void Awake()
    {
        _slider = transform.Find("Slider").GetComponent<Slider>();
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        _targetValue = health/maxHealth;
    }

    private void Update()
    {
        if (_slider.value > _targetValue)
        {
            _slider.value -= Time.deltaTime * 0.5f;
        }
        if (_slider.value < _targetValue)
        {
            _slider.value = _targetValue;
        }
    }
}
