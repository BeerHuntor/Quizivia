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
    [SerializeField] private float timeToAnswerQuestion = 30.0f; // Maybe add this as a setting to increase score and difficulty?

    private bool isTimerRunning;

    private float timer; 

    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _quizMaster = FindObjectOfType<QuizMaster>();
        timer = timeToAnswerQuestion;
        _quizMaster.IsAnsweringQuestion += ShouldTimerRun; 
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
            _uiManager.UpdateTimerFillSprite(timer, timeToAnswerQuestion);
            if (timer <= 0)
            {
                ShouldTimerRun(false);
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
    private void ShouldTimerRun(bool timerRunning)
    {
        isTimerRunning = timerRunning;
        if(timerRunning)
        {
            ResetTimer();
        }
    }

    ///<summary>
    /// Resets the timer to its default values.
    ///</summary>
    private void ResetTimer()
    {
        timer = timeToAnswerQuestion;
    }
}
