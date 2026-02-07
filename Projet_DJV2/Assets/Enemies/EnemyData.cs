using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData" , menuName = "ScriptableObjet/EnemyData" , order = 0)]
public class EnemyData : ScriptableObject
{
    public int score;
    public int maxHealth;
    public float speed;
    public float reward;
}

