using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Toy : MyMonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    #region Variables

    public ToyStats BaseStats { get; private set; }
    public ToyType ToyType { get; private set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public bool IsFacingRight { get; set; }
    public bool IsWithinAttackDistance { get; set; }

    protected Collider2D _collider;

    private ObjectPool<Toy> _toyPool;
    private ToyAttackDistanceCheck _attackDistanceCheck;
    private HealthBar _healthBar;
    private Animator _animator;

    #endregion

    #region State Machine
    [SerializeField] public ToyStateMachine StateMachine { get; set; }
    public ToyIdleState IdleState { get; set; }
    public ToyChaseState ChaseState { get; set; }
    public ToyAttackState AttackState { get; set; }
    public ToyHurtState HurtState { get; set; }
    public ToyDieState DieState { get; set; }
    public Transform Target { get; set; }
    public bool IsHavingTarget { get; set; }
    #endregion

    #region Start - Update - Fixed Update
    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentToyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentToyState.PhysicsUpdate();
    }
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
        Rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _attackDistanceCheck = GetComponentInChildren<ToyAttackDistanceCheck>();
        _healthBar = GetComponentInChildren<HealthBar>();
        SetupStateMachine();
    }

    private void SetupStateMachine()
    {
        StateMachine = new ToyStateMachine();
        IdleState = new ToyIdleState(this, StateMachine);
        ChaseState = new ToyChaseState(this, StateMachine);
        AttackState = new ToyAttackState(this, StateMachine);
        HurtState = new ToyHurtState(this, StateMachine);
        DieState = new ToyDieState(this, StateMachine);
    }

    public void SetStats(ToyStats stats)
    {
        BaseStats = stats;
        CurrentHealth = MaxHealth = stats.HealthPoint;
        _attackDistanceCheck.SetAttackDistance(stats.AttackRange);
        _healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
    }

    public void SetType(ToyType type) => ToyType = type;

    public void SetPool(ObjectPool<Toy> toyPool) => _toyPool = toyPool;


    #endregion

    #region Damageable
    public void TakeDamage(float damage)
    {
        Debug.Log("Toy Take Damage : " + damage);
        CurrentHealth -= damage;
        UpdateHealthBar();
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        StateMachine.ChangeState(DieState);
    }

    public void ReleaseToy()
    {
        _toyPool.Release(this);
    }

    public void UpdateHealthBar()
    {
        _healthBar.UpdateHealthBar(CurrentHealth, MaxHealth);
    }

    #endregion

    #region Toy Movement
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
        ToyIdle,
        ToyChase,
        ToyAttack,
        ToyHurt,
        ToyDie,
    }

    public void AnimationCallbackEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentToyState.AnimationCallbackEvent(triggerType);
    }

    public void TriggerAnimation(AnimationTriggerType triggerType)
    {
        switch (triggerType)
        {
            case AnimationTriggerType.ToyIdle:
                _animator.SetBool("isChasing", false);
                break;
            case AnimationTriggerType.ToyChase:
                _animator.SetBool("isChasing", true);
                break;
            case AnimationTriggerType.ToyAttack:
                _animator.SetTrigger("attack");
                break;
            case AnimationTriggerType.ToyHurt:
                break;
            case AnimationTriggerType.ToyDie:
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
        IsWithinAttackDistance = isWithinAttackDistance;
    }
    #endregion
}
