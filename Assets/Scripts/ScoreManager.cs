using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager", menuName = "Scriptable Objects/ScoreManager")]
public class ScoreManager : ScriptableObject
{
    public static ScoreManager instance;

    [SerializeField] internal float CurrentScore;

    [SerializeField] internal float playerLives;
}
