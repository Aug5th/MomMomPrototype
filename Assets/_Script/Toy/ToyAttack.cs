using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyAttack : MyMonoBehaviour
{
    protected ToyController _controller;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _controller = GetComponent<ToyController>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kid"))
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {

    }
}
