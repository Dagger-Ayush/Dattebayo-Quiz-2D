using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "QuestionSO", fileName = "Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea (2, 6)]
    [SerializeField] private string question = "Enter the question";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int CorrectAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return CorrectAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}
