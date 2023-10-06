using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MyMonoBehaviour
{
    [SerializeField] private Kid _target;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSprite();
        LoadTarget();
    }

    private void LoadSprite()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void LoadTarget()
    {
        _target = FindAnyObjectByType<Kid>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();   
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _movementSpeed * Time.fixedDeltaTime);
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
