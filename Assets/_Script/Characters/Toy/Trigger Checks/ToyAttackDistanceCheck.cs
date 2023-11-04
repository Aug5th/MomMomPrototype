using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyAttackDistanceCheck : MyMonoBehaviour
{
    private CircleCollider2D _circleCollider;
    [SerializeField] private Toy _toy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _circleCollider = GetComponent<CircleCollider2D>();
        _toy = GetComponentInParent<Toy>();
    }
    
    public void SetAttackDistance(float attackDistance)
    {
        _circleCollider.radius = attackDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !(_toy._isHealingMode))
        {
            //Debug.Log(_toy.name + " / OntriggerEnter2D");
            _toy.SetAttackDistanceBool(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log(_toy.name + " / OnCollisionStay2D");
            _toy.SetAttackDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log(_toy.name + " / OnTriggerExit2D");
            _toy.SetAttackDistanceBool(false);
        }
    }
}
