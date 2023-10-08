using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MyMonoBehaviour
{
    [SerializeField] private Kid _target;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private EnemyController _controller;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _controller = GetComponent<EnemyController>();
        _target = FindAnyObjectByType<Kid>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();   
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _controller.Enemy.BaseStats.MovementSpeed * Time.fixedDeltaTime);
        Vector2 direction = _target.transform.position - transform.position;
        FlipBody(direction.x);
    }

    private void FlipBody(float xDirection)
    {
        if (xDirection > 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (xDirection < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

}
