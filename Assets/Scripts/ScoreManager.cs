using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager", menuName = "Scriptable Objects/ScoreManager")]
public class ScoreManager : ScriptableObject
{
    public static ScoreManager instance;

    public static float CurrentScore = 0;
}
