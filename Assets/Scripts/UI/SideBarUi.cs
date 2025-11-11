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
}