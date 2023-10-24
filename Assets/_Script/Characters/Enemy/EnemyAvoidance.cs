using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAvoidance : MonoBehaviour
{
    public float avoidanceRadius = 0.1f;
    public float avoidanceForce = 0.01f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        List<Collider2D> nearbyEnemies = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // Find nearby enemy colliders
        if (Physics2D.OverlapCircle(transform.position, avoidanceRadius, filter, nearbyEnemies) > 0)
        {
            foreach (Collider2D enemyCollider in nearbyEnemies)
            {
                if (enemyCollider.gameObject != gameObject)
                {
                    Vector2 avoidDirection = (transform.position - enemyCollider.transform.position).normalized;
                    rb.AddForce(avoidDirection * avoidanceForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
