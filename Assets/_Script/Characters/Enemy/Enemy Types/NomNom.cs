using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomNom : Singleton<NomNom> , IDamageable
{
    public Transform Transform { get; private set; }
    public bool IsFacingRight { get; set; }
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public float NormalSpeed = 0.5f;
    public float SlowSpeed = 0.2f;
    public float MoveSpeed;

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
        MoveSpeed = NormalSpeed;
        Transform = transform;
        gameObject.SetActive(false);
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

    public void TakeDamage(float damage)
    {
        Debug.Log("Nom Nom Take damage");
        MoveSpeed = SlowSpeed;
        StartCoroutine(GetNormalSpeed());
    }

    private IEnumerator GetNormalSpeed()
    {
        yield return new WaitForSeconds(1);
        MoveSpeed = NormalSpeed;
    }

    public void Die()
    {
        // Nom Nom is invincible
    }

    public void UpdateHealthBar()
    {
        // Nom Nom don't have Healthbar
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Kid"))
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

}
