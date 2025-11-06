using System.Collections;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] bool isACorrect;
    [SerializeField] bool isBCorrect;
    [SerializeField] bool isCCorrect;
    [SerializeField] bool isDCorrect;
    [SerializeField] GameObject questionManager;
    
    [Header("Time until object destruction")]
    [SerializeField] float waitingTime;

    IEnumerator DestroyQuestion()
    {
        yield return new WaitForSeconds(waitingTime);
        
        Destroy(questionManager);
        Destroy(this.gameObject);
    }
    public void OnClickA()
    {
        if (isACorrect)
        {
            new ReplyMessage()
            {
                IsCorrectReply = true,
            }.InvokeExtension();
        }
        else
        {
            new ReplyMessage()
            {
                IsIncorrectReply = true,
            }.InvokeExtension();
        }

        StartCoroutine(DestroyQuestion());
    }
    public void OnClickB()
    {
        if (isBCorrect)
        {
            new ReplyMessage()
            {
                IsCorrectReply = true,
            }.InvokeExtension();
        }
        else
        {
            new ReplyMessage()
            {
                IsIncorrectReply = true,
            }.InvokeExtension();
        }
        StartCoroutine(DestroyQuestion());
    }
    public void OnClickC()
    {
        if (isCCorrect)
        {
            new ReplyMessage()
            {
                IsCorrectReply = true,
            }.InvokeExtension();
        }
        else
        {
            new ReplyMessage()
            {
                IsIncorrectReply = true,
            }.InvokeExtension();
        }
        StartCoroutine(DestroyQuestion());
    }
    public void OnClickD()
    {
        if (isDCorrect)
        {
            new ReplyMessage()
            {
                IsCorrectReply = true,
            }.InvokeExtension();
        }
        else
        {
            new ReplyMessage()
            {
                IsIncorrectReply = true,
            }.InvokeExtension();
        }
        StartCoroutine(DestroyQuestion());
    }
}
