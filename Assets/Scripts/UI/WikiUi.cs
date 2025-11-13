using TMPro;
using UnityEngine;

public class WikiUi : MonoBehaviour
{
    [Header("Object declaration in editor")]
    
    [SerializeField] GameObject wikiUi;
    [SerializeField] TMP_Text wikiText;
    
    [Header("Question texts")]

    [SerializeField] string question1;
    [SerializeField] string question2;
    [SerializeField] string question3;
    [SerializeField] string question4;
    [SerializeField] string question5;
    [SerializeField] string question6;
    [SerializeField] string question7;
    [SerializeField] string question8;
    [SerializeField] string question9;
    [SerializeField] string question10;
    
    Animator _wikiAnimator;
    
    private readonly int IsHiddenAnimWiki = Animator.StringToHash("IsHiddenAnim");

    void Awake()
    {
        _wikiAnimator = wikiUi.GetComponent<Animator>();
        _wikiAnimator.SetBool(IsHiddenAnimWiki, false);
        wikiUi.SetActive(false);
    }

    public void HideWikiUi()
    {
        _wikiAnimator.SetBool(IsHiddenAnimWiki, false);
    }

    void OnEnable()
    {
        Broker.Subscribe<QuestionCheatSheetMessage>(OnCheatMessageReceived);
    }

    void OnDisable()
    {
        Broker.Unsubscribe<QuestionCheatSheetMessage>(OnCheatMessageReceived);
    }

    void OnCheatMessageReceived(QuestionCheatSheetMessage msg)
    {
        if (!wikiUi.activeSelf)
        {
            wikiUi.SetActive(true);
        }
        _wikiAnimator.SetBool(IsHiddenAnimWiki, true);
        
        if (msg.hasClickedQuestion1)
        {
            wikiText.text = question1;
        }

        if (msg.hasClickedQuestion2)
        {
            wikiText.text = question2;
        }

        if (msg.hasClickedQuestion3)
        {
            wikiText.text = question3;
        }

        if (msg.hasClickedQuestion4)
        {
            wikiText.text = question4;
        }

        if (msg.hasClickedQuestion5)
        {
            wikiText.text = question5;
        }

        if (msg.hasClickedQuestion6)
        {
            wikiText.text = question6;
        }

        if (msg.hasClickedQuestion7)
        {
            wikiText.text = question7;
        }

        if (msg.hasClickedQuestion8)
        {
            wikiText.text = question8;
        }

        if (msg.hasClickedQuestion9)
        {
            wikiText.text = question9;
        }
    }
}
