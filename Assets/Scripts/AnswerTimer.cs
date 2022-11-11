using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Handles the logic for the countdown timer 
///</summary>

public class AnswerTimer : MonoBehaviour
{
    UIManager _uiManager;
    QuizMaster _quizMaster;
    GameManager _gameManager;
    SettingsManager _settingsManager;
    private bool isTimerRunning;

    private float timer;

    void Start()
    {
        Init();
    }
    private void Init()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _quizMaster = FindObjectOfType<QuizMaster>();
        _gameManager = FindObjectOfType<GameManager>();
        _settingsManager = SettingsManager.settings;
        timer = _settingsManager.GetTimeToAnswer();
        SubscribeToEvents();
        //isTimerRunning = true;
         
    }
    private void SubscribeToEvents()
    {
        _quizMaster.IsAnsweringQuestionEvent += ShouldTimerRunEvent;
    }

    private void UnscubscribeToEvents()
    {
        _quizMaster.IsAnsweringQuestionEvent -= ShouldTimerRunEvent;
    }
    private void OnEnable()
    {
        isTimerRunning = true;
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        isTimerRunning = false;
        UnscubscribeToEvents();
    }
    private void Update()
    {
        UpdateTimer();
    }

    ///<summary>
    /// Updates the countdown timer and calls the UIManager to update its text to display
    ///</summary>
    private void UpdateTimer()
    {   
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            _uiManager.SetCountownTimerText(Mathf.Round(timer));
            _uiManager.UpdateTimerFillSprite(timer, _settingsManager.GetTimeToAnswer());
            if (timer <= 0)
            {
                ShouldTimerRunEvent(false);
                _quizMaster.CheckAnswer(null);
            }
        }
        else
        {
            _uiManager.SetCountownTimerText(Mathf.Round(timer));
        }
    }

    ///<summary>
    /// Sets the bool in response to the IsAnsweringQuestion Event in QuizMaster to say wether the timer should be running or not.
    ///</summary>
    private void ShouldTimerRunEvent(bool timerRunning)
    {
        isTimerRunning = timerRunning;
        if (isTimerRunning)
        {
            ResetTimer();
        }
    }

    ///<summary>
    /// Resets the timer to its default values.
    ///</summary>
    private void ResetTimer()
    {
        timer = _settingsManager.GetTimeToAnswer();
    }
}
