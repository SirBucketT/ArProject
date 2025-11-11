using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    public void RestartScene()
    {
       SceneManager.LoadScene(0);
    }
}