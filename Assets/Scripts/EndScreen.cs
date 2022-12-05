using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    ScoreKeeper scorekeeper;

    private void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou Scored " + scorekeeper.CalculateScore() + "%";
    }
}
