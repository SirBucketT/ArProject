using UnityEngine;

public class MarkerLinkDownloaderUiButton : MonoBehaviour
{
    [SerializeField] private string targetURL; 

    public void OpenExternalLink()
    {
        Application.OpenURL(targetURL);
    }
}
