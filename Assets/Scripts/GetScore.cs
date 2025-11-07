using System;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    float DisplayScore, CurrentLives;
    
    [SerializeField] TMP_Text scoreText;
    
    private void Start()
    {
        DisplayScore = ScoreManager.instance.CurrentScore;
        CurrentLives = ScoreManager.instance.playerLives; 

        scoreText.text = $"Current Score: {DisplayScore.ToString()}";
    }

    void OnEnable()
    {
        Broker.Subscribe<ReplyMessage>(UpdateScore);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<ReplyMessage>(UpdateScore);
    }

    void UpdateScore(ReplyMessage msg)
    {
        if (msg.IsCorrectReply)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayScoreGain();
            }
            DisplayScore++;
            
        } else if (msg.IsIncorrectReply)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayBadPick();
            }
            DisplayScore--;
            CurrentLives--;
            ScoreManager.instance.playerLives = CurrentLives;
            OnLivesZero();
        }
        
        scoreText.text = $"Current Score: {DisplayScore.ToString()}";

        ScoreManager.instance.CurrentScore = DisplayScore;
    }

    void OnLivesZero()
    {
        
    }
}
