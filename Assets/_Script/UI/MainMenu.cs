using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MyMonoBehaviour
{

    [SerializeField] private AudioClip _mainMenuAudio;
    private void Start()
    {
        AudioSystem.Instance.PlayMusic(_mainMenuAudio);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
