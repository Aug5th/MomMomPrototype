using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MyMonoBehaviour
{

    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Health _health;

    public EnemyAnimation EnemyAnimation => _enemyAnimation;
    public Enemy Enemy => _enemy;
    public Health Health => _health;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _enemy = GetComponent<Enemy>();
        _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        _health = GetComponent<Health>();
    }
}
