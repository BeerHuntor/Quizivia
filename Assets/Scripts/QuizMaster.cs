using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizMaster : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] Button[] answerButtons = new Button[4];
    List<Question> myQuestions;
    private int currentQuestion;
    private string chosenAnswer;
    private int quizScore;
    private int pointsPerQuestionCorrect;
    private const int QuizSize = 2; // Set to a setting later - so user can chose amount of questions. 

    void Start()
    {
        GenerateQuiz();
        NextQuestion();
    }

    void GenerateQuiz()
    {
        Quiz myQuiz = new Quiz(QuizSize);
        myQuiz.Generate();
        quizScore = 0;
        currentQuestion = 0;
        myQuestions = myQuiz.GetQuestions();
    }
    private int GetCurrentQuestionNumber()
    {
        return currentQuestion;
    }
    /* Triggers the OnPointerClick Event on the chosen answer button. */
    public void OnClicked(Button buttonClicked)
    {
        chosenAnswer = buttonClicked.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    }
    /* Checks the answer selected against the correct answer */
    public void CheckAnswer()
    {
        if (chosenAnswer == myQuestions[GetCurrentQuestionNumber()].Correct_Answer)
        {
            AddScore(pointsPerQuestionCorrect);
        }
        currentQuestion++;
        NextQuestion();
    }

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
            questionText.text = myQuestions[qNum].QuizQuestion;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = myQuestions[qNum].QuestionAnswers[i];
            }
        }
    }

    private void AddScore(int scoreToAdd)
    {
        quizScore += scoreToAdd;
    }
    public int GetScore()
    {
        return quizScore;
    }

}