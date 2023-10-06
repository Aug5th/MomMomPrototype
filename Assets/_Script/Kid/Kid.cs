using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MyMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(1);
        }
    }

}
