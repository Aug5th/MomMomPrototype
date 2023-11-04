using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Kid"))
        {
            GameManager.Instance.UpdateGameState(GameState.Victory);
        }
    }
}
