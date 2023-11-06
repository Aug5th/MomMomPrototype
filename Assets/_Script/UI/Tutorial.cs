using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Singleton<Tutorial>
{
    public void GoToPhaseOne()
    {
        GameManager.Instance.UpdateGameState(GameState.PhaseOne);
    }
}
