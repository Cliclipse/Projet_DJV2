using Enemies;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WaveData" , menuName = "ScriptableObjet/WaveData" , order = 3)]

    public class WaveData : ScriptableObject
    {
        public EnemyController[] enemies;
    }
}
