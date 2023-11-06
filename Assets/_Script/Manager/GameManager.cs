using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameState GameState;
    public static event Action<GameState> OnStateChanged;

    [SerializeField] private GameObject _phaseOnePanel;
    [SerializeField] private GameObject _phaseTwoPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _tutorialPanel;

    [SerializeField] private GameObject _phaseOneCamera;
    [SerializeField] private GameObject _phaseTwoCamera;

    [SerializeField] private AudioClip _gameScreenAudio;
    [SerializeField] private AudioClip _winScreenAudio;
    [SerializeField] private AudioClip _loseScreenAudio;

    private bool _switchCamera = true;

    private void Start()
    {
        UpdateGameState(GameState.Tutorial);
    }

    private void Update()
    {
        if (EnemySpawnTrigger.Instance.EnemySpawning && _switchCamera)
        {
            _phaseOneCamera.SetActive(false);
            _phaseTwoCamera.SetActive(true);
            _switchCamera = false;
        }
    }
    public void UpdateGameState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.PhaseOne:
                HandlePhaseOneState();
                break;
            case GameState.PhaseTwo:
                HandlePhaseTwoState();
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
            case GameState.Victory:
                HandleVictoryState();
                break;
            case GameState.Tutorial:
                HandleTutorialState();
                break;
            default:
                break;
        }
        OnStateChanged?.Invoke(newState);
    }

    private void HandleTutorialState()
    {
        AudioSystem.Instance.PlayMusic(_gameScreenAudio);
        _tutorialPanel.SetActive(true);
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
        _phaseOnePanel.SetActive(false);
        _phaseTwoPanel.SetActive(false);
    }

    private void HandleVictoryState()
    {
        PauseGame();
        AudioSystem.Instance.PlayMusic(_winScreenAudio);
        _losePanel.SetActive(false);
        _winPanel.SetActive(true);
        _tutorialPanel.SetActive(false);
        _phaseOnePanel.SetActive(false);
        _phaseOneCamera.SetActive(false);
        _phaseTwoPanel.SetActive(false);
        _phaseTwoCamera.SetActive(false);
    }

    private void HandleLoseState()
    {
        AudioSystem.Instance.PlayMusic(_loseScreenAudio);
        PauseGame();
        _losePanel.SetActive(true);
        _winPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        _phaseOnePanel.SetActive(false);
        _phaseOneCamera.SetActive(false);
        _phaseTwoPanel.SetActive(false);
        _phaseTwoCamera.SetActive(false);
    }

    private void HandlePhaseTwoState()
    {
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
        _phaseOnePanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        //_phaseOneCamera.SetActive(false);
        _phaseTwoPanel.SetActive(true);
        //_phaseTwoCamera.SetActive(true);
        Kid.Instance.UpdateHealthBar();
        Teddy.Instance.IsAttackMode = true;
    }

    private void HandlePhaseOneState()
    {
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        _phaseOnePanel.SetActive(true);
        _phaseOneCamera.SetActive(true);
        _phaseTwoPanel.SetActive(false);
        _phaseTwoCamera.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

public enum GameState
{
    PhaseOne = 0,
    PhaseTwo = 1,
    Lose = 2,
    Victory = 3,
    Tutorial = 4,
}
