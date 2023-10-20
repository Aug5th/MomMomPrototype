using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MyMonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    #region Variables
    public EnemyStats BaseStats { get; private set; }
    public EnemyType EnemyType { get; private set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsWithinAttackDistance { get; set; }

    protected Collider2D _collider;


    private ObjectPool<Enemy> _enemyPool;
    private EnemyAttackDistanceCheck _enemyAttackDistanceCheck;
    private HealthBar _healthBar;
    #endregion

    #region Start - Update - Fixed Update
    private void Start()
    {
        StateMachine.Initialize(ChaseState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    #endregion

    #region State Machine
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyHurtState HurtState { get; set; }
    public EnemyDieState DieState { get; set; }

    private Animator _animator;
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
        Rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _enemyAttackDistanceCheck = GetComponentInChildren<EnemyAttackDistanceCheck>();
        _healthBar = GetComponentInChildren<HealthBar>();
        SetupStateMachine();
    }

    private void SetupStateMachine()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        HurtState = new EnemyHurtState(this, StateMachine);
        DieState = new EnemyDieState(this, StateMachine);
    }

    public void SetStats(EnemyStats stats)
    {
        BaseStats = stats;
        CurrentHealth = MaxHealth = stats.HealthPoint;
        _enemyAttackDistanceCheck.SetAttackDistance(stats.AttackRange);
        _healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
    }

    public void SetType(EnemyType type) => EnemyType = type;

    public void SetPool(ObjectPool<Enemy> enemyPool) => _enemyPool = enemyPool;

    #endregion

    #region Damageable
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        //_controller.HealthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        StateMachine.ChangeState(DieState);
        //_controller.EnemyAnimation.TriggerAnimationDie();
    }

    public void ReleaseEnemy()
    {
        _enemyPool.Release(this);
    }

    public void UpdateHealthBar()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Enemy Movement
    public void Move(Vector2 velocity)
    {
        Rigidbody.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #region Animation Trigger
    public enum AnimationTriggerType
    {
        EnemyIdle,
        EnemyChase,
        EnemyAttack,
        EnemyHurt,
        EnemyDie,
    }

    public void AnimationCallbackEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationCallbackEvent(triggerType);
    }

    public void TriggerAnimation(AnimationTriggerType triggerType)
    {
        switch (triggerType)
        {
            case AnimationTriggerType.EnemyIdle:
                _animator.SetBool("isChasing", false);
                break;
            case AnimationTriggerType.EnemyChase:
                _animator.SetBool("isChasing", true);
                break;
            case AnimationTriggerType.EnemyAttack:
                _animator.SetTrigger("attack");
                break;
            case AnimationTriggerType.EnemyHurt:
                break;
            case AnimationTriggerType.EnemyDie:
                _animator.SetTrigger("die");
                break;
            default:
                break;
        }
    }
    #endregion

    #region Distance Checks
    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        Debug.Log("IsWithinAttackDistance = "+isWithinAttackDistance);
        IsWithinAttackDistance = isWithinAttackDistance;
    }
    #endregion

}


