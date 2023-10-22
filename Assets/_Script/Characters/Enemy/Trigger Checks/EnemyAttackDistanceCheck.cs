using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDistanceCheck : MyMonoBehaviour
{
    private CircleCollider2D _circleCollider;
    [SerializeField] private Enemy _enemy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _circleCollider = GetComponent<CircleCollider2D>();
        _enemy = GetComponentInParent<Enemy>();
    }
    
    public void SetAttackDistance(float attackDistance)
    {
        _circleCollider.radius = attackDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Toy") || collision.CompareTag("Kid"))
        {
            _enemy.SetAttackDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Toy") || collision.CompareTag("Kid"))
        {
            _enemy.SetAttackDistanceBool(false);
        }
    }
}
