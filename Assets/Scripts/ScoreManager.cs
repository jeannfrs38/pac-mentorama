using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;
    private int _highScore;

    public int HighScore { get => _highScore; }
    public int CurrentScore { get => _currentScore; }

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;
    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("high-score", 0);
    }
    void Start()
    {
        var allCollectibles = FindObjectsOfType<Collectible>();
        foreach (Collectible collectible in allCollectibles)
        {
            collectible.OnCollected += Collectible_OnCollected;
        }


    }

    private void Collectible_OnCollected(int score, Collectible collectible)
    {
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);
        if (_currentScore >= _highScore)
        {
            _highScore = _currentScore;
            OnHighScoreChanged?.Invoke(_highScore);
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("high-score", _highScore);
    }
}
