using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen() 
    {
        return questionsSeen;
    }

    public void IncrementQuestionSeen()
    {
        questionsSeen++;
    }

    public float CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers/ (float)questionsSeen * 100);
    }
}
