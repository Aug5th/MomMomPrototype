using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Pathfinding;

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
    public Path PathMap;
    public int CurrentWayPoint = 1;

    protected Collider2D _collider;

    private ObjectPool<Toy> _toyPool;
    private ToyAttackDistanceCheck _attackDistanceCheck;
    private HealthBar _healthBar;
    private Animator _animator;
    private Seeker _seeker;
    public bool IsHealingMode;
    
    protected Transform _attackPoint;

    [SerializeField] protected LayerMask _targetLayerMask;

    #endregion

    #region State Machine
    [SerializeField] public ToyStateMachine StateMachine { get; set; }
    public ToyIdleState IdleState { get; set; }
    public ToyChaseState ChaseState { get; set; }
    public ToyAttackState AttackState { get; set; }
    public ToyHurtState HurtState { get; set; }
    public ToyDieState DieState { get; set; }
    public Transform Target { get; set; }
    public bool IsActivated { get; set; }
    public bool IsInHealingZone { get; set; }
    public float CurrentSpeed { get; set;}
    #endregion

    #region Start - Update - Fixed Update
    private void Start()
    {
        IsHealingMode = false;
        StateMachine.Initialize(IdleState);
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update()
    {
        FindTarget();
        if(IsHealingMode && Vector2.Distance(Rigidbody.position, Kid.Instance.Transform.position) <= 0.2f)
        {
            SetIsInHealingZone(true);
        }
        else
        {
            SetIsInHealingZone(false);
        }

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
        _seeker = GetComponent<Seeker>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _attackDistanceCheck = GetComponentInChildren<ToyAttackDistanceCheck>();
        _healthBar = GetComponentInChildren<HealthBar>();
        _attackPoint = transform.Find("AttackPoint");
        _targetLayerMask = LayerMask.GetMask("Enemy");
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
        IsActivated = false;
        IsInHealingZone = false;
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
    public virtual void StartAttack()
    {

    }

    public virtual void EndAttack()
    {

    }
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

    private void UpdatePath()
    {
        if(_seeker.IsDone() && Target != null)
        {
            _seeker.StartPath(Rigidbody.position, Target.position, OnPathComplete);
        }
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("A path was calculated. Did it fail with an error_Toy? " + p.error);

        if (!p.error) {
            PathMap = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            CurrentWayPoint = 1;
        }
    }


    private void FindTarget()
    {
        if(!IsActivated)
        {
            return;
        }

        if(IsHealingMode) // If healing mode, go to kid
        {
           Target = Kid.Instance.Transform;
        }
        else
        {
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
            if (allTargets.Length > 0)
            {
                Target = allTargets[0].transform;
                foreach (GameObject target in allTargets)
                {
                    if (Vector2.Distance(Rigidbody.position, target.transform.position) < Vector2.Distance(Rigidbody.position, Target.transform.position))
                    {                            
                        Target = target.transform;
                    }

                }
            }
            else
            {
                Target = NomNom.Instance.Transform;
                // target Nom Nom
            }       
        }
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

    private void Healing()
    {
        CurrentHealth += Mathf.Min(BaseStats.HealingStrength, MaxHealth-CurrentHealth);
        UpdateHealthBar();
        if (CurrentHealth == MaxHealth)
        {
            CancelInvoke("Healing");
            //IsHealingMode = false;
        }
    }

    public void SetHealingMode(bool healingMode) // Set healing mode
    {
        if(!IsActivated)
        {
            return;
        }
        IsHealingMode = healingMode;
        StateMachine.ChangeState(ChaseState);
    }

    private void OnTriggerEnter2D(Collider2D collision) // Change to chase state when kid touch the toy
    {
        if(collision.CompareTag("Kid"))
        {
            if(!IsActivated)
            {
                SetActivated(true);
                StateMachine.ChangeState(ChaseState);
            }
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

    public void SetActivated(bool isActivated)
    {
        IsActivated = isActivated;
    }

    public void SetIsInHealingZone(bool isInHealingZone)
    {
        if(IsInHealingZone == isInHealingZone)
        {
            return;
        }

        IsInHealingZone = isInHealingZone;

        if(IsInHealingZone)
        {
            Move(Vector2.zero);
            InvokeRepeating("Healing", 0f, 0.5f);
        }
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
                _animator.Play("die");
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
