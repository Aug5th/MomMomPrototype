using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MyMonoBehaviour
{
    private Animator _animator;
    private EnemyController _controller;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
        _controller = GetComponentInParent<EnemyController>();
    }

    public void TriggerAnimationDie()
    {
        _animator.SetTrigger("die");
    }

    public void TriggerAnimationAttack()
    {
        _animator.SetBool("isAttack",true);
    }

    public void EndAttack()
    {
        _animator.SetBool("isAttack", false);
        _controller.EnemyAttack.EndAttack();
    }

    public void ReleaseEnemy()
    {
        _controller.Enemy.ReleaseEnemy();
    }
}
