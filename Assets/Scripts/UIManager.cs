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
    QuizMaster _quizMaster;
    SettingsManager _settingsManager;
    GameManager _gameManager;

    [Header("Canvas")]
    [SerializeField] private Canvas titleScreenCanvas; // index 0 -- SwitchCanvas() Method
    [SerializeField] private Canvas settingScreenCanvas; // index 1 -- SwitchCanvas() Method
    [SerializeField] private Canvas quizScreenCanvas; // index 2 -- SwitchCanvas() Method
    [SerializeField] private Canvas endOfQuizCanvas; // index 4 -- SwitchCanvas() Method

    [Header("Quiz Proper")]
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

    [Header("Settings Menu")]
    [Header("Question Settings Slider")]
    [SerializeField] Slider questionSettingsSlider;
    [SerializeField] TextMeshProUGUI questionSettingsSliderMainText;
    [SerializeField] TextMeshProUGUI questionSettingsSliderValueText;
    [Header("Answer Time Settings")]
    [SerializeField] Slider answerSettingsSlider;
    [SerializeField] TextMeshProUGUI answerSettingsSliderMainText;
    [SerializeField] TextMeshProUGUI answerSettingsSliderValueText;
    


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SwitchCanvas(titleScreenCanvas);
    }

    ///<summary>
    /// Initialises everything needed at the start of a game. 
    ///</summary>
    void Init()
    {
        _quizMaster = FindObjectOfType<QuizMaster>();
        _gameManager = FindObjectOfType<GameManager>();
        _settingsManager = new SettingsManager();
        _quizMaster.OnCorrectAnswerEvent += ChangeCorrectAnswerSprite;
        _gameManager.OnSwitchCanvasEvent += OnSwitchCanvasEvent;
    }

    #region QuizProper
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
        progressBar.maxValue = _settingsManager.GetQuizSize();
        progressBar.value = (progressBar.maxValue - _quizMaster.GetQuestionsSeen());
        // if (_quizMaster.GetQuestionsSeen() == progressBar.maxValue)
        // {
        //     Image[] fillAmount = progressBar.gameObject.GetComponentsInChildren<Image>();
        //     foreach (Image image in fillAmount)
        //     {
        //         Debug.Log(image.gameObject.name);
        //     }
        // }
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
    #endregion

    #region SettingsMenu
    ///<summary>
    /// Initializes the settings menu by calling Methods which set the UI Elements.
    ///</summary>
    public void InitSettingsMenuScreen()
    {
        BaseQuestionSettings();
        BaseAnswerTimeSettings();
    }
    ///<summary>
    /// Sets the base values of the QuestionSettingsSlider and accompanying text when this canvas is switched.
    ///</summary>
    private void BaseQuestionSettings()
    {
        questionSettingsSlider.maxValue = _settingsManager.GetQuestionDatabaseSize();
        questionSettingsSlider.value = _settingsManager.GetQuizSize();
        questionSettingsSliderMainText.text = "How Many Questions In Quiz?";
        questionSettingsSliderValueText.text = questionSettingsSlider.value.ToString();
    }
    ///<summary>
    /// Updates the slider value and accompanying text when OnSliderChange Event fires from the slider.
    ///</summary>
    public void UpdateQuestionSlider()
    {
        questionSettingsSliderValueText.text = questionSettingsSlider.value.ToString();
        _settingsManager.SetQuizSize((int)questionSettingsSlider.value);
    }
    ///<summary>
    /// Sets the base values of the AnswerTimeSlider and accompanying text when this canvas is switched.
    ///</summary>
    private void BaseAnswerTimeSettings()
    {
        answerSettingsSlider.maxValue = _settingsManager.GetMaxTimeToAnswer();
        answerSettingsSlider.value = _settingsManager.GetTimeToAnswer();
        answerSettingsSliderMainText.text = "Seconds to answer each question?";
        answerSettingsSliderValueText.text = answerSettingsSlider.value.ToString();
    }
    ///<summary>
    /// Updates the slider value and accompanying text when OnSliderChange event fires from the slider.
    ///</summary>
    public void UpdateAnswerSlider()
    {
        answerSettingsSliderValueText.text = answerSettingsSlider.value.ToString();
        _settingsManager.SetTimeToAnswer((int)answerSettingsSlider.value);
    }

    #endregion

    #region TitleScreen
    #endregion

    #region SwitchCanvas
    ///<summary>
    /// Finds all the canvas in the game including inactive and sets them all to inactive aside from the background canvas, 
    ///then sets the desired canvas to active
    ///</summary>
    public void SwitchCanvas(Canvas canvasToSwitch)
    {
        Canvas[] foundCanvas = FindObjectsOfType<Canvas>(true);

        foreach (Canvas canvas in foundCanvas)
        {
            if (canvas.name != "Background_Canvas")
            {
                canvas.gameObject.SetActive(false);
            }
        }

        canvasToSwitch.gameObject.SetActive(true);
    }
    ///<summary>
    /// Method which is called when the SwitchCanvasEvent is fired on the click of the play button
    ///</summary>
    public void OnSwitchCanvasEvent(int canvasIndex)
    {

        switch (canvasIndex)
        {
            case 0:
                SwitchCanvas(titleScreenCanvas);
                break;
            case 1:
                SwitchCanvas(settingScreenCanvas);
                InitSettingsMenuScreen(); //Not being called by the buttons
                break;
            case 2:
                SwitchCanvas(quizScreenCanvas);
                break;
            case 4:
                SwitchCanvas(endOfQuizCanvas);
                break;
        }

    }
    #endregion


}
