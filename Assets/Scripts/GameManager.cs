using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Responsible for bringing everything together and talking to the different systems. Handles the stopping and starting of the game
///</summary>
public class GameManager : MonoBehaviour
{
    private UIManager _uiMangager; 
    private QuizMaster _quizMaster;
    private SettingsManager _settingsManager;

    ///<summary>
    /// An Event which is invoked when the player presses the play button.
    ///</summary>
    public event Action<int> OnStartGameEvent; 
    ///<summary>
    /// An Event which is invoked when the canvas needs to be changed.
    ///</summary>
    public event Action<int> OnSwitchCanvasEvent; 
    public bool isGameRunning {get; protected set;}


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    ///<summary>
    /// Initialises everything needed at the start of a game. 
    ///</summary>
    void Init()
    {
        _uiMangager = FindObjectOfType<UIManager>();
        _quizMaster = FindObjectOfType<QuizMaster>();
        _settingsManager = new SettingsManager();
    }

    ///<summary>
    /// Called to trigger the event when the play button is pressed, and passes it to the QuizMaster.
    ///</summary>
    public void StartQuiz()
    {
        isGameRunning = true;
        OnStartGameEvent?.Invoke(_settingsManager.GetQuizSize());
        OnSwitchCanvasEvent?.Invoke(2);
    }

    public void SetIsGameRunning(bool isRunning)
    {
        isGameRunning = isRunning;
        if (isGameRunning == false)
        {
            OnSwitchCanvasEvent?.Invoke(4);
        }
    }

    public bool GetIsGameRunning()
    {
        return isGameRunning;
    }

}
