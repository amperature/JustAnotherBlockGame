using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pauseMessage;
    [SerializeField] private TextMeshProUGUI jabg;
    public static GameBehavior Instance;
    public Utilities.GameplayState State;

    private int _score;
    public int Score

        {
        // Getter property
        get => _score;
        
        // Setter property
        set
        {
            _score = value;
            _scoreUI.text = Score.ToString();
        }
    }
    [SerializeField] private TextMeshProUGUI _scoreUI;

    void Awake()
    {
        // Singleton pattern (took this from the Game Behavior script in pong)
        
        // When creating an instance, check if one already exists,
        // and if the existing is the one that is trying to be created.
        if (Instance != null && Instance != this)
        {
            // If a different instance already exists,
            // please destroy the instance that is currently being created.
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }        
        
    }

    void Start() {
        State = Utilities.GameplayState.Play;
        _pauseMessage.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            State = State == Utilities.GameplayState.Play
                ? Utilities.GameplayState.Pause
                : Utilities.GameplayState.Play;
            _pauseMessage.enabled = !_pauseMessage.enabled;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitApp();
        }
    }

    public void GameOver() {
        State = Utilities.GameplayState.Pause;
        //_pauseMessage.text = "Game Over.";
        jabg.text = "Game Over. Press Q to Quit";
    }
    public void ScorePoint()
    {
        Score++;
    }
    
    public void LosePoint() 
    {
        Score--;
    }

    void QuitApp()
    {
        #if UNITY_EDITOR
            // Set variable if game is running from Unity
            EditorApplication.isPlaying = false;
        #else
            // Function will be called if game is running from a build
            Application.Quit();
        #endif
    }
}
