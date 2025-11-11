using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLives : MonoBehaviour
{
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject Hp1, Hp2, Hp3;
    
    [SerializeField] Color livesColor, livesLostColor;
    
    void OnEnable()
    {
        Broker.Subscribe<LivesUpdateMessage>(UpdateCurrentHealth);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<LivesUpdateMessage>(UpdateCurrentHealth);
    }

    void UpdateCurrentHealth(LivesUpdateMessage msg)
    {
        livesText.text = $"Lives: {msg.currentLives.ToString()}";
       
        if (msg.currentLives == 3)
        {
            Hp1.GetComponent<Image>().color = livesColor;
            Hp2.GetComponent<Image>().color = livesColor; 
            Hp3.GetComponent<Image>().color = livesColor;
        }
        else if (msg.currentLives == 2)
        {
            Hp1.GetComponent<Image>().color = livesColor;
            Hp2.GetComponent<Image>().color = livesColor; 
            Hp3.GetComponent<Image>().color = livesLostColor;
        }
        else if (msg.currentLives == 1)
        {
            Hp1.GetComponent<Image>().color = livesColor;
            Hp2.GetComponent<Image>().color = livesLostColor; 
            Hp3.GetComponent<Image>().color = livesLostColor;
        } else if  (msg.currentLives == 0)
        {
            new GameOverMessage()
            {
                isGameOver = true
            }.InvokeExtension();
        }
    }
}
