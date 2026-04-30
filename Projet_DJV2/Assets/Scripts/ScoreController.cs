using ScriptableObjects;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int minScoreMultiplier = -5;
    [SerializeField] private int maxScoreMultiplier = 5;
    
    private int _score;
    private int _scoreMultiplier = 1;
    
    /// <summary>
    /// Score de la partie
    /// </summary>
    public int Score => _score;

    /// <summary>
    /// Le joueur tue un ennemi
    /// </summary>
    /// <param name="enemy">Données de l'ennemi tué</param>
    public void KillEnemy(EnemyData enemy)
    {
        if (_scoreMultiplier <= 0)
        {
            _scoreMultiplier = 1;
        }
        else
        {
            _scoreMultiplier = Mathf.Clamp(_scoreMultiplier + 1, minScoreMultiplier, maxScoreMultiplier);
        }
        _score += enemy.score * _scoreMultiplier;
    }

    /// <summary>
    /// Un ennemi arrive à la fin du chemin
    /// </summary>
    /// <param name="enemy">Données de l'ennemi</param>
    public void EnemyReachCastle(EnemyData enemy)
    {
        if (_scoreMultiplier >= 0)
        {
            _scoreMultiplier = -1;
        }
        else
        {
            _scoreMultiplier = Mathf.Clamp(_scoreMultiplier - 1, minScoreMultiplier, maxScoreMultiplier);
        }
        _score += enemy.score * _scoreMultiplier;
    }
}
