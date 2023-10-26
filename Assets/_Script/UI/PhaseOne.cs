using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : MonoBehaviour
{
    public void StartPhaseTwo()
    {
        GameManager.Instance.UpdateGameState(GameState.PhaseTwo);
    }
}
