using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MyMonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
        _enemy = GetComponentInParent<Enemy>();
    }

    public void TriggerAnimationDie()
    {
        _animator.SetTrigger("die");
    }

    public void ReleaseEnemy()
    {
        _enemy.ReleaseEnemy();
    }
}
