using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ExternalAPIQuestionManager1 : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] string question;
    
    [Header("Question Answers")]
    
    [SerializeField] string answerA;
    [SerializeField] string answerB;
    [SerializeField] string answerC;
    
    string answerD;
    
    [Header("TextMesh components")]
    
    [SerializeField] TMP_Text answerTextA;
    [SerializeField] TMP_Text answerTextB;
    [SerializeField] TMP_Text answerTextC;
    [SerializeField] TMP_Text answerTextD;
    [SerializeField] TMP_Text questionText;

    void Start()
    {
        
        StartCoroutine(UpdateLocationWhenReady());
        
        questionText.text = question;
        answerTextA.text = answerA;
        answerTextB.text = answerB;
        answerTextC.text = answerC;
        answerTextD.text = answerD;
    }
    
    IEnumerator UpdateLocationWhenReady()
    {
        while (string.IsNullOrEmpty(TestLocationService.instance.location))
            yield return null;

        answerD = TestLocationService.instance.location;
        answerTextD.text = answerD;
    }
}
