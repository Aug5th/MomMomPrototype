using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MyMonoBehaviour
{
    [SerializeField] private ToyAnimation _toyAnimation;
    [SerializeField] private Toy _toy;
    [SerializeField] private Health _health;

    public ToyAnimation ToyAnimation => _toyAnimation;
    public Toy Toy => _toy;
    public Health Health => _health;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _toy = GetComponent<Toy>();
        _toyAnimation = GetComponentInChildren<ToyAnimation>();
        _health = GetComponent<Health>();
    }
}
