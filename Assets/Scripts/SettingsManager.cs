using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Responsible for handling the settings of the game
///</summary>

public class SettingsManager
{
    private const int DEFAULT_QUIZ_SIZE = 15;
    public int UserSelectedQuizSize { get; protected set; }
    private const int DEFAULT_TIME_TO_ANSWER = 15;
    private const int MAX_TIME_TO_ANSWER = 30;
    public int UserDefinedTimeToAnswer { get; protected set; }

    ///<summary>
    /// Current size of the questions in the JSON file. 
    ///</summary>
    const int QUESTION_DATABASE_LENGTH = 50; // Number of questions in Questions.JSON

    ///<summary>
    /// Returns the QuizSize setting if one was set, if not defaults to the default setting.
    public int GetQuizSize()
    {
        if (UserSelectedQuizSize != 0)
        {
            return UserSelectedQuizSize;
        }
        else
        {
            return DEFAULT_QUIZ_SIZE;
        }
    }

    public void SetQuizSize(int size)
    {
        UserSelectedQuizSize = size;
    }
    ///<summary>
    /// Returns the TimeToAnswer setting if one was set,  if not defaults to the default setting.
    ///</summary>
    public int GetTimeToAnswer()
    {
        if (UserDefinedTimeToAnswer != 0)
        {
            return UserDefinedTimeToAnswer;
        }
        else
        {
            return DEFAULT_TIME_TO_ANSWER;
        }
    }
    ///<summary>
    /// Sets the TimeToAnswer setting in the settings menu - If one was given. 
    ///</summary>
    public void SetTimeToAnswer(int time)
    {
        UserDefinedTimeToAnswer = time;
    }
    ///<summary>
    /// Returns the number of questions in the JSON question database.
    ///</summary>
    public int GetQuestionDatabaseSize()
    {
        return QUESTION_DATABASE_LENGTH;
    }
    ///<summary>
    /// Returns the max time allowed to answer a question.
    ///</summary>
    public int GetMaxTimeToAnswer()
    {
        return MAX_TIME_TO_ANSWER;
    }
}
