using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pauseMessage;
    [SerializeField] private TextMeshProUGUI jabg;
    //private TextMeshProUGUI 
    public static GameBehavior Instance;
    public Utilities.GameplayState State;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pieceDrop;
    [SerializeField] private AudioClip lineClear;
    private TetrisBlock CurrentPiece;

    private int _score;
    public bool PieceDrop = false;
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
    private int _progress;
    public int Progress

        {
        // Getter property
        get => _progress;
        
        // Setter property
        set
        {
            _progress = value;
            _progressUI.text = Progress.ToString();
        }
    }
    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private TextMeshProUGUI _progressUI;

    //int i = 1;
    IEnumerator MyCoroutine() {

        //i += 1;
        //Debug.Log(i);
        
        //CurrentPiece.transform.RotateAround(CurrentPiece.transform.TransformPoint(CurrentPiece.rotationPoint), new Vector3(0, 0, -1), 180);
        
        FindAnyObjectByType<PieceController>().RollRoll();

        //PieceController.Instance.RollRoll();
        
        yield return null;

    }

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
            GameObject.DontDestroyOnLoad(gameObject);
        }        
        
        GameObject PieceMover = GameObject.Find("PieceMovement");
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
        if (Input.GetKeyDown(KeyCode.H)) {
            Scene scene = SceneManager.GetActiveScene();  
            SceneManager.LoadScene("Home");
            }
        if (Progress > 100 && Progress < 105 || Progress > 200 && Progress < 205) {
            Debug.Log("brug");
            StartCoroutine(MyCoroutine());
        }

    }

    public void GameOver() {
        State = Utilities.GameplayState.Pause;
        jabg.text = "Game Over. Press R to Restart or Q to Quit.";
    }
    public void ScorePoint()
    {
        Score++;
        audioSource.PlayOneShot(lineClear);
    }
    
    public void LosePoint() 
    {
        Score--;
    }

    public void AddProgress() {
        Progress++;
        audioSource.PlayOneShot(pieceDrop);
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
