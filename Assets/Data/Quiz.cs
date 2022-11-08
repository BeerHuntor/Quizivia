using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class Quiz
{

    public Quiz(int quizSize)
    {
        this._QuizSize = quizSize;
        if (chosenQuestionsThisRound != null)
        {
            chosenQuestionsThisRound.Clear();
        }
    }

    const int QUESTION_DATABASE_LENGTH = 50; // Number of questions in Questions.JSON
    const string jsonPath = "Assets/Data/Questions.JSON";
    private int _QuizSize;
    private List<Question> quizQuestions = new List<Question>(); // Selected Question object list that is passed to the game.
    private List<int> chosenQuestionsThisRound = new List<int>(); // Logic sanity check to make sure no repeat questions are selected
    private List<string> questionElements = new List<string>(); // Dynamic list of elements that is cleared once looped through
    private static string jsonString = File.ReadAllText(jsonPath);
    private static JSONNode data = JSON.Parse(jsonString);
    
    /* Method to call to generate the questions for this round/game. */
    public void Generate()
    {
        if (data != null)
        {
            for (int i = 0; i < _QuizSize; i++)
            {
                int index = GenerateQuestionIndex();
                if (!chosenQuestionsThisRound.Contains(index))
                {
                    LoopThroughElements(index);
                }
                else
                {
                    LoopThroughElements(GenerateQuestionIndex());
                    Debug.LogWarning("Quizivia:: Generating New Question -- Duplicate Selection!");
                }
            }
        }
        else
        {
            Debug.LogError("Quizivia:: Question Data Not Found!");
            return;
        }
    }

    /* Public get method to get the quiz questions for this round. */
    public List<Question> GetQuestions()
    {
        if (quizQuestions.Count != _QuizSize)
        {
            Debug.LogError("Quizivia:: Quiz Didn't Generate Properly! -- Try Generating The Quiz Again.");
        }
        return quizQuestions;
    }
    
    /* Generates random int to pass to the Questions.JSON to select a question from the json.*/
    private int GenerateQuestionIndex()
    {
        int questionIndex;
        return questionIndex = Random.Range(1, QUESTION_DATABASE_LENGTH);
    }
    /* If question selection is valid, loops through the elements of the selected question and passes them to a list to be 
       read from later. */
    private void LoopThroughElements(int index)
    {
        chosenQuestionsThisRound.Add(index);
        foreach (JSONNode question in data["question" + index])
        {
            AddElementsToList(question.Value);
        }
        ConstructQuestionFromLists();
        ClearElementsFromList();
    }

    /* Generates a new Question object from the valid selection and adds the Question objet to a list for the round.*/
    private void ConstructQuestionFromLists()
    {
        if (questionElements.Count != 6)
        {
            Debug.LogError("Quizivia:: Question Elements Did Not Generate! -- Element Count: " + questionElements.Count);
            return;
        }
        Question q = new Question(questionElements[0], questionElements[1], questionElements[2], questionElements[3],
                                    questionElements[4], questionElements[5]);

        quizQuestions.Add(q);
    }
    /* List to enable a logic check to ensure we don't have any repeat questions, storing the question number as an int */
    private List<int> ChosenQuestion(int chosenQuestion)
    {
        chosenQuestionsThisRound.Add(chosenQuestion);
        return chosenQuestionsThisRound;
    }
    /* Adds the chosen selected question elements to a list to be read from */ 
    private void AddElementsToList(string element)
    {
        questionElements.Add(element);
    }
    /* Clears the dynamic list of elements that is read from after generating the quetsion object.*/
    private void ClearElementsFromList()
    {
        questionElements.Clear();
    }

}
