using System;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] float CurrentLives;

    private void Start()
    {
        new LivesUpdateMessage()
        {
            currentLives = CurrentLives,
        }.InvokeExtension();
    }

    void OnEnable()
    {
        Broker.Subscribe<ReplyMessage>(UpdateLivesInternal);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<ReplyMessage>(UpdateLivesInternal);
    }

    void UpdateLivesInternal( ReplyMessage msg)
    {
        if (msg.IsIncorrectReply)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayBadPick();
            }
            CurrentLives--;
            
            new LivesUpdateMessage()
            {
                currentLives = CurrentLives,
            }.InvokeExtension();
            
            OnLivesZero();
        }
    }
    
    void OnLivesZero()
    {
        if (CurrentLives <= 0)
        {
            CurrentLives = 0; 
            
            new LivesZeroMessage()
            {
                isLivesZero = true,
            }.InvokeExtension();
        }
    }
}
