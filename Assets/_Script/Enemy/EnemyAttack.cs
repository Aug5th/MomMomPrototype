using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MyMonoBehaviour
{
    protected EnemyController controller;
    [SerializeField] protected Transform attackPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        controller = GetComponent<EnemyController>();
        attackPoint = transform.Find("AttackPoint");
    }

    public virtual void StartAttack() { }
    public virtual void EndAttack() { }
}
