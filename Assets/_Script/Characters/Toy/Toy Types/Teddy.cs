using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : Singleton<Teddy>
{
    [SerializeField] private Animator _animator;
    private bool _shoot = true;
    private Transform _attackPoint;

    public float MoveSpeed = 1f;
    public float NormalSpeed = 1f;
    public float SlowSpeed = 0.5f;
    public bool IsFacingRight { get; set; }
    public bool IsAttackMode {get; set;}
    // Start is called before the first frame update
    protected override void LoadComponents()
    {
        base.LoadComponents();
        _animator = GetComponent<Animator>();
        _attackPoint = transform.Find("AttackPoint");
        MoveSpeed = NormalSpeed;
    }
    private void Start()
    {
        _animator.Play("idle");
        IsFacingRight = true;
        IsAttackMode = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(!_shoot || !IsAttackMode)
        {
            return;
        }
        Camera cam = Camera.main;
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        _animator.Play("attack");
        Vector2 direction = worldPosition - this.transform.position;
        CheckForLeftOrRightFacing(direction);
        // spawn bullet
        var bullet = ProjectileSpawner.Instance.SpawnTeddyBullet();
        bullet.transform.SetPositionAndRotation(_attackPoint.position, _attackPoint.rotation);
        bullet.transform.right = direction;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bullet.BaseStats.Speed;
       

        StartCoroutine(ShootDelay());
        _shoot = false;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1f);
        _shoot = true;
    }

    private void CheckForLeftOrRightFacing(Vector2 velocity)
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

    public void StopMoving()
    {
        _animator.Play("idle");
    }

    public void Move(Vector3 destination)
    {
        Vector2 direction = (destination - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
        _animator.Play("walk");
        CheckForLeftOrRightFacing(direction);
    }
}
