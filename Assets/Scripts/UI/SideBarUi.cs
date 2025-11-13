using UnityEngine;

public class SideBarUi : MonoBehaviour
{
    [SerializeField] GameObject sideBar;
    
    Animator sidebarAnimator;
    
    private readonly int IsHiddenAnim = Animator.StringToHash("IsHidden");

    private void Awake()
    {
        sidebarAnimator = sideBar.GetComponent<Animator>();
        sidebarAnimator.SetBool(IsHiddenAnim, true);
        sideBar.SetActive(false);
    }
    
    
    public void ShowSideBar()
    {
        sideBar.SetActive(true);
        sidebarAnimator.SetBool(IsHiddenAnim, false);
    }
    
    public void HideSideBar()
    {
        sidebarAnimator.SetBool(IsHiddenAnim, true);
    }
    
    public void Question1() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true);
        new QuestionCheatSheetMessage(){hasClickedQuestion1 = true}.InvokeExtension();
    }
    public void Question2() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true);
        new QuestionCheatSheetMessage(){hasClickedQuestion2 = true}.InvokeExtension();
    }
    public void Question3() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion3 = true}.InvokeExtension();
    }
    public void Question4() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion4 = true}.InvokeExtension();
    }
    public void Question5() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion5 = true}.InvokeExtension();
    }
    public void Question6() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion6 = true}.InvokeExtension();
    }
    public void Question7() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion7 = true}.InvokeExtension();
    }
    public void Question8() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion8 = true}.InvokeExtension();
    }
    public void Question9() 
    { 
        sidebarAnimator.SetBool(IsHiddenAnim, true); 
        new QuestionCheatSheetMessage(){hasClickedQuestion9 = true}.InvokeExtension();
    }
}