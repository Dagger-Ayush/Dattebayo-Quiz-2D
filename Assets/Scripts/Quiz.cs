using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField ] List<QuestionSO> question = new List<QuestionSO>();
    QuestionSO currentQuestion;
    private int correctAnswerIndex;

    [Header("Buttons")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoringText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    private EndScreen endScreen;
    public bool hasAnsweredEarly = true;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        progressBar.maxValue = question.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if(timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                endScreen.ShowFinalScore();
                return;
            }

            GetNextQuestion();
            hasAnsweredEarly = false;
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void DisplayQuestionAndAnswers()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI optionText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            optionText.text = currentQuestion.GetAnswer(i);
        }
    }

    // Function call in the inspector if button is clicked
    private void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;

        DisplayAnswer(index);
        // set all option state to false (no longer interactable for that question)
        SetButtonState(false);
        // reset timer
        timer.CancelTimer();

        scoringText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            // changing the button sprite when correct answer is chosen
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;

            questionText.text = "Correct!";
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            // if the wrong answer is chosen get the correct answer index and text
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswerText = currentQuestion.GetAnswer(correctAnswerIndex);

            // changing question text to correct answer
            questionText.text = "Sorry the correct answer is: \n" + correctAnswerText;

            // changing the button image if wrong answer is chosen
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    private void GetNextQuestion()
    {
        if (question.Count != 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestionAndAnswers();
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }   
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, question.Count);
        currentQuestion = question[index];

        if (question.Contains(currentQuestion))
            question.Remove(currentQuestion);
    }

    void SetButtonState (bool state)
    {
        for (int  i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image currentButtonImage = answerButtons[i].GetComponent<Image>();
            currentButtonImage.sprite = defaultAnswerSprite;
        }
    }
}
