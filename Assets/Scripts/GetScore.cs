using System;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    private float DisplayScore;
    
    private void Start()
    {
        DisplayScore = ScoreManager.CurrentScore;
    }
}
