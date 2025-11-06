using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] bool isACorrect;
    [SerializeField] bool isBCorrect;
    [SerializeField] bool isCCorrect;
    [SerializeField] bool isDCorrect;

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
    }
}
