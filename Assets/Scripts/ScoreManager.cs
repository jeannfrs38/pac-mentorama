using System;
<<<<<<< HEAD
using System.Runtime.Serialization;
=======
>>>>>>> 4888a89a114d3073f682cd5af81302a550d62510
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;
    private int _highScore;
<<<<<<< HEAD
    private int combo;
    private int ghostScore = 200;
=======
>>>>>>> 4888a89a114d3073f682cd5af81302a550d62510

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
<<<<<<< HEAD

        var ghosts = FindObjectsOfType<GhostAI>();
        foreach (GhostAI ghost in ghosts)
        {
            ghost.OnDefeated += Ghost_OnDefeated;
            ghost.OnGhostStateChanged += Ghost_OnGhostStateChanged;    
        }
=======
>>>>>>> 4888a89a114d3073f682cd5af81302a550d62510
        var allCollectibles = FindObjectsOfType<Collectible>();
        foreach (Collectible collectible in allCollectibles)
        {
            collectible.OnCollected += Collectible_OnCollected;
        }


    }
<<<<<<< HEAD
    private void Ghost_OnDefeated()
    {
        if (combo < 4)
        {
            combo += 1;
           
        
        }
        int value = combo * ghostScore;
          _currentScore += value;
          Debug.Log(value);
            OnScoreChanged?.Invoke(_currentScore);
           if (_currentScore >= _highScore)
           {
            _highScore = _currentScore;
            OnHighScoreChanged?.Invoke(_highScore);
           }
    }
    private void Ghost_OnGhostStateChanged(GhostState ghostState)
    {
        if (ghostState == GhostState.Active)
        {
            combo = 0;
        }
    }
=======

>>>>>>> 4888a89a114d3073f682cd5af81302a550d62510
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
<<<<<<< HEAD
    private void Update() {
         Debug.Log(combo);
    }
=======
>>>>>>> 4888a89a114d3073f682cd5af81302a550d62510
}
