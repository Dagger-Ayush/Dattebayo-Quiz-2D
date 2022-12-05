using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToGiveTheAnswer = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;
    float timerValue;

    public bool loadNextQuestion = false;
    public bool isAnsweringQuestion = false; 
    public float fillFraction;

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        // When Player is not answering the question
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToGiveTheAnswer;
            }
            else if (timerValue <= 0) // When timer ends
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else if(!isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else if (timerValue <= 0) // When timer ends
            {
                isAnsweringQuestion = true;
                timerValue = timeToGiveTheAnswer;
                loadNextQuestion = true;
            }
        }

        timerValue -= Time.deltaTime;
    }
}
