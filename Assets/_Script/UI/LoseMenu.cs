using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseMenu : MyMonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        GameManager.Instance.ResumeGame();
    }
}
