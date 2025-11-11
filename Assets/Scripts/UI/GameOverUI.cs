using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

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
        }
    }
}
