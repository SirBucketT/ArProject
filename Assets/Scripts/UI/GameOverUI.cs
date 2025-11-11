using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject XR_rig;
    
    [SerializeField] TMP_Text scoreText, highscoreText;
    float scoreValue, highscoreValue;

    string GetScoreDisplay;

    void Awake()
    {
        gameOverScreen.SetActive(false);
        XR_rig.SetActive(true);
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
            XR_rig.SetActive(false);

            scoreValue = GetScore.instance.DisplayScore;

            GetScoreDisplay = scoreValue.ToString();
            
            scoreText.text = $"Your Score {GetScoreDisplay}";

            HighscoreCheck();
        }
    }

    void HighscoreCheck()
    {
        highscoreValue = PlayerPrefs.GetFloat("Highscore");
        
        if (highscoreValue > scoreValue)
        {
            PlayerPrefs.SetFloat("Highscore", scoreValue);
            PlayerPrefs.Save();
            
            highscoreValue = PlayerPrefs.GetFloat("Highscore");
        }
        
        highscoreText.text = $"Your Highscore: {highscoreValue.ToString()}";
    }
}
