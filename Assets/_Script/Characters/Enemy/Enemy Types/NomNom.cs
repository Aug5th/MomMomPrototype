using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomNom : Singleton<NomNom>
{
    public Transform Transform { get; private set; }
    public bool IsFacingRight { get; set; }
    public float MoveSpeed = 1f;
    // Start is called before the first frame update
    [SerializeField] private Animator _animator;

    public void Move(Vector3 destination)
    {
        Vector2 direction = (destination - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
        _animator.Play("walk");
        CheckForLeftOrRightFacing(direction);
    }

    public void StopMoving()
    {
        _animator.Play("idle");
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
        Transform = transform;
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
}
