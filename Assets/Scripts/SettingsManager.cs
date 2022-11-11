using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Responsible for handling the settings of the game
///</summary>

public class SettingsManager
{
    private int QuizSize = 15;
    private int TimeToAnswer = 10;

    public int GetQuizSize()
    {
        return QuizSize;
    } 

    public void SetQuizSize(int size)
    {
        QuizSize = size;
    }

    public int GetTimeToAnswer()
    {
        return TimeToAnswer;
    }

    public void SetTimeToAnswer(int time)
    {
        TimeToAnswer = time;
    }
}
