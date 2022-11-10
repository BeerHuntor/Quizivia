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
    [Header("QuizUI")]
    [SerializeField] private Button[] answerButtons = new Button[4];
    [SerializeField] private TextMeshProUGUI questionText; 
    [SerializeField] private Sprite correctAnswerSprite;
    [SerializeField] private Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerSprite;
    private float timerFillFraction; 

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Progress Bar")]
    [SerializeField] private Slider progressBar;

    ///<summary>
    /// Holds the selected button which was pressed this question, to enable the switching of sprites if there was a correct answer.
    ///</summary>
    private Button selectedButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _quizMaster = FindObjectOfType<QuizMaster>();
        _quizMaster.OnCorrectAnswer += ChangeCorrectAnswerSprite;    
    }


    ///<summary>
    /// Called from QuizMaster, responsible for changing the question text on screen.
    ///</summary>
    public void ChangeQuestionText(string question)
    {
        questionText.text = question;
        UpdateProgressBar();
    }

    ///<summary>
    /// Called from QuizMaster, responsible for changing the answer button texts on screen.
    ///</summary>
    public void ChangeAnswerText(string[] answers)
    {
        SetDefautAnswerButtonSprites();
        IsButtonsInteractable(true);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
        }
    }

    ///<summary>
    /// Sets the button sprites to their default sprites - pre correct answer.
    ///</summary>
    private void SetDefautAnswerButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    ///<summary>
    /// Updates the progress bar's fill amount to highlight the amount of questions left there are in the quiz. 
    ///</summary>
    private void UpdateProgressBar()
    {
        progressBar.maxValue = _quizMaster.GetQuizSize();
        progressBar.value = ( progressBar.maxValue - _quizMaster.GetQuestionsSeen() ); 
    }

    ///<summary>
    /// Triggers OnClick Event on the button pressed, grabs the text from the component on the button and passes it to 
    /// the QuizMaster's method call to check the answer. 
    ///</summary>
    public void GetAnswerFromButton(Button buttonClicked)
    {
        selectedButton = buttonClicked;
        IsButtonsInteractable(false);
        string answerText = answerText = buttonClicked.GetComponentInChildren<TextMeshProUGUI>().text;
        _quizMaster.CheckAnswer(answerText);
    }

    ///<summary>
    /// Toggles the interactability on the answer buttons when called.
    ///</summary>
    private void IsButtonsInteractable(bool interactable)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = interactable;
        }
    }
    ///<summary>
    /// Sets the countdown timers text to the value passed to it
    ///</summary>
    public void SetCountownTimerText(float time)
    {
        timerText.text = time.ToString();
    }
    ///<summary>
    /// Updates the timer fill sprite based on the current timer value from AnswerTimer.
    ///</summary>
    public void UpdateTimerFillSprite(float currentTimer, float answerQuestionTime)
    {
        timerFillFraction = currentTimer / answerQuestionTime; 
        timerSprite.fillAmount = timerFillFraction;
    }
    ///<summary>
    /// Sets and updates the score text. 
    ///</summary>
    public void SetScoreText(int scoreToShow)
    {
        scoreText.text = "Score: " + scoreToShow + "%";
    }

    ///<summary>
    /// Fires when the OnCorrectAnswer Event is triggered in QuizMaster.  Changes the correct answer sprite to show selected answer. 
    ///</summary>
    private void ChangeCorrectAnswerSprite()
    {
        selectedButton.GetComponent<Image>().sprite = correctAnswerSprite;
    }
}
