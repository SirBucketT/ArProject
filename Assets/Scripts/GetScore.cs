using System;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    private float DisplayScore;
    
    [SerializeField] TMP_Text scoreText;
    
    private void Start()
    {
        DisplayScore = ScoreManager.CurrentScore;

        scoreText.text = DisplayScore.ToString(); 
    }
}
