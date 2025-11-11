using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    
    [SerializeField] TMP_Text scoreText, highscoreText;
    float scoreValue, highscoreValue;

    void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    
    void OnEnable()
    {
        Broker.Subscribe<GameOverMessage>(OnGameOverMessaggeObtained);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<GameOverMessage>(OnGameOverMessaggeObtained);
    }

    void OnGameOverMessaggeObtained(GameOverMessage msg)
    {
        if (msg.isGameOver)
        {
            gameOverScreen.SetActive(true);

            if (GetScore.instance != null)
            {
                scoreValue = GetScore.instance.DisplayScore;
            }
            else
            {
                scoreValue = 0f; 
            }
            
            scoreText.text = $"Your Score {scoreValue:0}";

            HighscoreCheck();
        }
    }

    void HighscoreCheck()
    {
        highscoreValue = PlayerPrefs.GetFloat("Highscore", 0f); 
        
        if (scoreValue > highscoreValue)
        {
            PlayerPrefs.SetFloat("Highscore", scoreValue);
            PlayerPrefs.Save();
            
            highscoreValue = scoreValue; 
        }
        
        highscoreText.text = $"Your Highscore: {highscoreValue:0}";
    }
}