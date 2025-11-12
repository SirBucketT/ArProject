using System;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    public static GetScore instance;
    
    [SerializeField] ScoreManager scoreManager;

    public float DisplayScore;
    
    [SerializeField] TMP_Text scoreText;
    
    void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
    private void Start()
    {
        ScoreManager.CurrentScore = 0f;
        
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
        Handheld.Vibrate();
        
        if (msg.IsCorrectReply)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayScoreGain();
            }
            DisplayScore++;
            
        }
        
        ScoreManager.CurrentScore = DisplayScore;
        scoreText.text = $"Current Score: {DisplayScore.ToString()}";
    }
}
