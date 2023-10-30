using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyAttackState : ToyState
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Transform _attackPoint;
    private float _attackCircle = 0.2f;

    private float _timer;

    public ToyAttackState(Toy toy, ToyStateMachine toyStateMachine) : base(toy, toyStateMachine)
    {
        _targetLayerMask = LayerMask.GetMask("Enemy");
        _attackPoint = toy.transform.Find("AttackPoint");
    }

    public override void AnimationCallbackEvent(Toy.AnimationTriggerType triggerType)
    {
        base.AnimationCallbackEvent(triggerType);
        if(triggerType == Toy.AnimationTriggerType.ToyAttack)
        {
            EndAttack();
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        _timer = toy.BaseStats.AttackSpeed + 1f;
        toy.Move(Vector2.zero);
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyIdle);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        toy.Move(Vector2.zero);
        toy.TriggerAnimation(Toy.AnimationTriggerType.ToyAttack);
        StartAttack();
        if (!toy.IsWithinAttackDistance)
        {
            toyStateMachine.ChangeState(toy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void StartAttack()
    {
        if(_timer > toy.BaseStats.AttackSpeed)
        {
            _timer = 0f;
            Debug.Log("Toy Attack");
            toy.TriggerAnimation(Toy.AnimationTriggerType.ToyAttack);
        }
        _timer += Time.deltaTime;
        
    }

    public void EndAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircle, _targetLayerMask);
        if (enemiesToDamage.Length > 0)
        {
            Debug.Log("enemiesToDamage " + enemiesToDamage.Length);
            var damageable = enemiesToDamage[0].GetComponent<IDamageable>();  // get 0 means the toy only attack single target
            if (damageable != null)
            {
                damageable.TakeDamage(toy.BaseStats.Power);
                Debug.Log("Enemy take damage: " + toy.BaseStats.Power);
            }
        }
    }
}
