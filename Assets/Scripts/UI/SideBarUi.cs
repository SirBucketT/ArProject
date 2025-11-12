using UnityEngine;

public class SideBarUi : MonoBehaviour
{
    [SerializeField] GameObject sideBar;
    
    Animator sidebarAnimator;
    
    private readonly int IsHiddenHash = Animator.StringToHash("IsHidden");

    private void Awake()
    {
        sidebarAnimator = sideBar.GetComponent<Animator>();
        sidebarAnimator.SetBool(IsHiddenHash, true);
        sideBar.SetActive(false);
    }
    
    public void ShowSideBar()
    {
        sideBar.SetActive(true);
        sidebarAnimator.SetBool(IsHiddenHash, false);
    }
    
    public void HideSideBar()
    {
        sidebarAnimator.SetBool(IsHiddenHash, true);
    }
    
    public void Question1() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true);
        new QuestionCheatSheetMessage(){hasClickedQuestion1 = true}.InvokeExtension();
    }
    public void Question2() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true);
        new QuestionCheatSheetMessage(){hasClickedQuestion2 = true}.InvokeExtension();
    }
    public void Question3() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion3 = true}.InvokeExtension();
    }
    public void Question4() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion4 = true}.InvokeExtension();
    }
    public void Question5() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion5 = true}.InvokeExtension();
    }
    public void Question6() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion6 = true}.InvokeExtension();
    }
    public void Question7() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion7 = true}.InvokeExtension();
    }
    public void Question8() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion8 = true}.InvokeExtension();
    }
    public void Question9() 
    { 
        sidebarAnimator.SetBool(IsHiddenHash, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion9 = true}.InvokeExtension();
    }
}