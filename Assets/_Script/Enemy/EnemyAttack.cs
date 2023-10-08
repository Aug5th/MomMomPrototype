using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MyMonoBehaviour
{
    protected EnemyController _controller;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _controller = GetComponent<EnemyController>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy Attack");
        if (collision.tag == "Kid")
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {

    }
}
