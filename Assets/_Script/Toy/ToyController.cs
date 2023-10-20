using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MyMonoBehaviour
{
    [SerializeField] private ToyAnimation _toyAnimation;
    [SerializeField] private Toy _toy;
    [SerializeField] private HealthBar _healthBar;

    public ToyAnimation ToyAnimation => _toyAnimation;
    public Toy Toy => _toy;
    public HealthBar HealthBar => _healthBar;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _toy = GetComponent<Toy>();
        _toyAnimation = GetComponentInChildren<ToyAnimation>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }
}
