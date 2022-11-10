using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

///<summary>
/// Defines what a quiz is. Pulls data from JSON files and populates the quiz via Question objects which are passed
/// their desired properties. 
///</summary>
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

    ///<summary>
    /// Current size of the questions in the JSON file. 
    ///</summary>
    const int QUESTION_DATABASE_LENGTH = 50; // Number of questions in Questions.JSON
    const string jsonPath = "Assets/Data/Questions.JSON";
    ///<summary>
    /// Number passed to the constructor to generate a quiz of desired amount of questions.
    ///</summary>
    private int _QuizSize;
    private List<Question> quizQuestions = new List<Question>(); // Selected Question object list that is passed to the game.
    private List<int> chosenQuestionsThisRound = new List<int>(); // Logic sanity check to make sure no repeat questions are selected
    private List<string> questionElements = new List<string>(); // Dynamic list of elements that is cleared once looped through
    private static string jsonString = File.ReadAllText(jsonPath);
    private static JSONNode data = JSON.Parse(jsonString);
    
    ///<summary>
    /// Generates a quiz based on desired size and calls other methods to build the quiz in full. 
    ///</summary>
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

    ///<summary>
    /// Returns the populated list of Question Objects for the round. 
    ///</summary>
    public List<Question> GetQuestions()
    {
        if (quizQuestions.Count != _QuizSize)
        {
            Debug.LogError("Quizivia:: Quiz Didn't Generate Properly! -- Try Generating The Quiz Again.");
        }
        return quizQuestions;
    }
    
    ///<summary>
    /// Generates a random number to select a random question within the Questions.JSON
    ///</summary>
    private int GenerateQuestionIndex()
    {
        int questionIndex;
        return questionIndex = Random.Range(1, QUESTION_DATABASE_LENGTH);
    }

    ///<summary>
    /// Loops through the elements of the chosen question in the Questions.JSON file and passes them to a list. Then calls
    /// the CreateQuestionFromLists() method to populate the Question object. Then clears the dynamic list of elements for the
    /// next pass, should there be one. 
    ///</summary>
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

    ///<summary>
    /// Generates a new Question and passes to its constructer the valid elements of the selected question from the json file.
    ///</summary>
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
    ///<summary>
    /// Indexes and returns the chosen questions to ensure no repeat questions are trying to be added to the Quiz.
    ///</summary>
    private List<int> ChosenQuestion(int chosenQuestion)
    {
        chosenQuestionsThisRound.Add(chosenQuestion);
        return chosenQuestionsThisRound;
    }
    ///<summary>
    /// Adds each element of the selected question from the json file into a list.
    ///</summary>
    private void AddElementsToList(string element)
    {
        questionElements.Add(element);
    }
    ///<summary>
    /// Clears the element list of the selected question elements from the json file.
    ///</summary>
    private void ClearElementsFromList()
    {
        questionElements.Clear();
    }

}
