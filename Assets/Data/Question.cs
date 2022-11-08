using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public Question(string question, string a1, string a2, string a3, string a4, string correctAnswer)
    {
        this.QuizQuestion = question; 
        this.Answer_One = a1;
        this.Answer_Two = a2;
        this.Answer_Three = a3; 
        this.Answer_Four = a4;
        this.Correct_Answer = correctAnswer;
        this.questionAnswers = AnswersToArray();
    }
    public string QuizQuestion { get; protected set;}
    public string Answer_One { get; protected set; }
    public string Answer_Two { get; protected set; }
    public string Answer_Three { get; protected set; } 
    public string Answer_Four {get; protected set; }
    public string Correct_Answer { get; protected set; }
    public string[] QuestionAnswers { get { return questionAnswers; } protected set {questionAnswers = value; } }
    private string[] questionAnswers; 

    // Added answers to an array to save re-writing code for the UI in QuizMaster
    private string[] AnswersToArray()
    {
        return new string[] {Answer_One, Answer_Two, Answer_Three, Answer_Four};
    }

}