using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject XRrig;
    
    [SerializeField] TMP_Text scoreText;

    string GetScoreDisplay;

    void Awake()
    {
        gameOverScreen.SetActive(false);
        XRrig.SetActive(true);
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
            XRrig.SetActive(false);

            GetScoreDisplay = GetScore.instance.DisplayScore.ToString();
            
            scoreText.text = $"Your Score {GetScoreDisplay}";
        }
    }
}
