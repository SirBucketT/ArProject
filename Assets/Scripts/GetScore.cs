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
            ScoreManager.CurrentScore = DisplayScore;
            scoreText.text = DisplayScore.ToString(); 
        } else if (msg.IsIncorrectReply)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayBadPick();
            }
            
            DisplayScore--;
            ScoreManager.CurrentScore = DisplayScore;
            scoreText.text = DisplayScore.ToString(); 
            
            //TODO: will implement a system with lives or gameover condition later. 
        }
    }
}
