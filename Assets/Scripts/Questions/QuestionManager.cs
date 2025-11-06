using System;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] string question;
    
    [Header("Question Answers")]
    
    [SerializeField] string answerA;
    [SerializeField] string answerB;
    [SerializeField] string answerC;
    [SerializeField] string answerD;
    
    [Header("TextMesh components")]
    
    [SerializeField] TMP_Text answerTextA;
    [SerializeField] TMP_Text answerTextB;
    [SerializeField] TMP_Text answerTextC;
    [SerializeField] TMP_Text answerTextD;
    [SerializeField] TMP_Text Question;

    void Start()
    {
        Question.text = question;
        answerTextA.text = answerA;
        answerTextB.text = answerB;
        answerTextC.text = answerC;
        answerTextD.text = answerD;
    }
}
