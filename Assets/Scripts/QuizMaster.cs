using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Handles all logic of the quiz, checking answers, keeping score and initialising the quiz setup. 
///</summary>

public class QuizMaster : MonoBehaviour
{
    UIManager _uiManager; 
    ///<summary>
    /// An Event that invokes when a correct answer is selected.
    ///</summary>
    public event Action OnCorrectAnswer;
    ///<summary>
    /// An Event that is invoked when player is answering a question and when has answered a question
    ///</summary>
    public event Action<bool> IsAnsweringQuestion;
    List<Question> myQuestions;
    private int currentQuestion;
    private int quizScore;
    private int pointsPerQuestionCorrect;
    private float cooldownBetweenQuestions = 3.0f;
    private const int QuizSize = 2; // Set to a setting later - so user can chose amount of questions. 
    
    // Unsure on this, people may get offended!.
    private string[] incorrectBerates = new string[] {
        "You silly goose.", 
        "Even a child would know that one!",
        "HAHA - WRONG!",
        "Here wear this pointy hat with a D on it!",
        "And here I thought you were smart..",
        "Oooo, so close...yet so far.",
        "Deffinately not smarter than a 10 year old!",
        "Oooops, try again.",
        "Did you actually go to school?",
        "If you had a brain you would be dangerous.",
        "You sound like you have 9 toes.",
        "Your knowledge is receding like your hairline.",
        "If there was 2 answers, you would still get it wrong.",
        "2+2 = 5 -- Quick Maffs.",
        "There are chimps that are smarter than you.",
        "Praying couldn't help you answer this one.",
        "I bet you ate dirt as a kid.",
        "LOL."
    }; 


    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        GenerateQuiz();
        NextQuestion();
    }

    ///<summary>
    /// Generates a new quiz with the desired size of questions and calls to grab the question list from the Quiz object.
    /// Initialises values needed for a new quiz, score, currentQuestionIndex etc
    ///</summary>
    void GenerateQuiz()
    {
        Quiz myQuiz = new Quiz(QuizSize);
        myQuiz.Generate();
        quizScore = 0;
        currentQuestion = 0;
        myQuestions = myQuiz.GetQuestions();
    }

    ///<summary>
    /// Gets the current question number to pass to other methods. 
    ///</summary>
    private int GetCurrentQuestionNumber()
    {
        return currentQuestion;
    }

    ///<summary>
    /// Checks the selected answer against the correct answer of the question and handles the response. 
    ///</summary>
    public void CheckAnswer(string answer)
    {
        if (answer == myQuestions[GetCurrentQuestionNumber()].Correct_Answer)
        {
            AddScore(pointsPerQuestionCorrect);
            Debug.Log("Correct!");
            OnCorrectAnswer?.Invoke();
        } 
        else 
        {
            _uiManager.ChangeQuestionText(GetIncorrectSaying());
        }
        currentQuestion++;
        IsAnsweringQuestion?.Invoke(false);
        StartCoroutine(WaitForCooldown());
    }

    ///<summary>
    /// Grabs the details of the next question to be shown and passes them off to the UIManager to display.
    /// Also checks to see if we have run out of questions in the quiz and calls GameOver.
    ///</summary>
    private void NextQuestion()
    {
        int qNum = GetCurrentQuestionNumber();
        if (qNum > (myQuestions.Count - 1)) // -1 due to zero index
        {
            Debug.LogWarning("Quizivia:: You answered all the questions -- Well Done!");
            return;
        }
        else
        {
            _uiManager.ChangeQuestionText(myQuestions[qNum].QuizQuestion);
            _uiManager.ChangeAnswerText(myQuestions[qNum].QuestionAnswers);
            IsAnsweringQuestion?.Invoke(true);
        }
    }

    ///<summary>
    /// Adds to the quizScore by the amount passed into the argument
    ///</summary>
    private void AddScore(int scoreToAdd)
    {
        quizScore += scoreToAdd;
    }

    ///<summary>
    /// Returns the current quizScore
    ///</summary>
    public int GetScore()
    {
        return quizScore;
    }

    ///<summary>
    /// Issues a cooldown between answering a question and the next based of cooldownBetweenQuestions
    ///</summary>
    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(cooldownBetweenQuestions);
        NextQuestion();
    }

    ///<summary>
    /// Returns random string from the incorrectSayings array to pass show the player on incorrect choice
    ///</summary>
    private string GetIncorrectSaying()
    {
        int index = UnityEngine.Random.Range(0,incorrectBerates.Length);
        return incorrectBerates[index];
    }

}