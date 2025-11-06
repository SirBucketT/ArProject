using System;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Question Data")]
    [SerializeField] string question;
    
    [SerializeField] string answerA;
    [SerializeField] string answerB;
    [SerializeField] string answerC;
    [SerializeField] string answerD;
    
    [SerializeField] TMP_Text answerTextA;
    [SerializeField] TMP_Text answerTextB;
    [SerializeField] TMP_Text answerTextC;
    [SerializeField] TMP_Text answerTextD;

    void Start()
    {
        answerTextA.text = answerA;
        answerTextB.text = answerB;
        answerTextC.text = answerC;
        answerTextD.text = answerD;
    }
}
