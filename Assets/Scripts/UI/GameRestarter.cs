using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(currentSceneName);
    }
}