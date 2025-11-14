using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    float completedQuestionCounter; 
    
    [SerializeField] GameObject WinScreen;

    void Awake()
    {
        WinScreen.SetActive(false);
    }
    
    void OnEnable()
    {
        Broker.Subscribe<ReplyMessage>(OnCompletedQuizQuestion);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<ReplyMessage>(OnCompletedQuizQuestion);
    }

    void OnCompletedQuizQuestion(ReplyMessage msg)
    {
        if (msg.IsCorrectReply)
        {
            completedQuestionCounter++;
        }

        if (completedQuestionCounter >= 9)
        {
            WinScreen.SetActive(true);
        }
    }
}