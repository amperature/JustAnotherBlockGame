using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


#if UNITY_EDITOR
    using UnityEditor;
#endif

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pauseMessage;
    public static UIBehavior Instance;
    public Utilities.GameplayState State;

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
