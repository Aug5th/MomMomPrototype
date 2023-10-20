using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MyMonoBehaviour
{
    [SerializeField] private Slider _slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _slider = GetComponentInChildren<Slider>();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth / maxHealth;
    }
}
