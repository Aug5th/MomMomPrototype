using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : Singleton<EnemySpawnTrigger>
{
    public bool EnemySpawning { get; set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kid"))
        {
            Debug.Log("EnemySpawnTrigger Spawn ok");
            EnemySpawning = true;
            NomNom.Instance.gameObject.SetActive(true);
        }
    }
}
