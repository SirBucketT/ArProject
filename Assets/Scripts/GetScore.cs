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
            
            //TODO: will implement a system with lives or gameover condition later.
            
        }
        
        scoreText.text = $"Current Score: {DisplayScore.ToString()}";
        
        DisplayScore = ScoreManager.CurrentScore;
    }
}
