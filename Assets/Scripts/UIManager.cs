using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

///<summary>
/// Handles all the UI elements from the game. 
///</summary>

public class UIManager : MonoBehaviour
{
    private QuizMaster _quizMaster; 
    [SerializeField] Button[] answerButtons = new Button[4];
    [SerializeField] TextMeshProUGUI questionText; 
    // Start is called before the first frame update
    void Start()
    {
        _quizMaster = FindObjectOfType<QuizMaster>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///<summary>
    /// Triggers OnClick Event on the button pressed, grabs the text from the component on the button and passes it to 
    /// the QuizMaster's method call to check the answer. 
    ///</summary>
    public void GetAnswerFromButton(Button buttonClicked)
    {
        string answerText = answerText = buttonClicked.GetComponentInChildren<TextMeshProUGUI>().text;
        _quizMaster.CheckAnswer(answerText);
    }

    ///<summary>
    /// Called from QuizMaster, responsible for changing the question text on screen.
    ///</summary>
    public void ChangeQuestionText(string question)
    {
        questionText.text = question;
    }

    ///<summary>
    /// Called from QuizMaster, responsible for changing the answer button texts on screen.
    ///</summary>
    public void ChangeAnswerText(string[] answers)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
        }
    }
}
