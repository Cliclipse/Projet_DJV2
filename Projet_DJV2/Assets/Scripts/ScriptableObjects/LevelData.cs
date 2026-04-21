using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData" , menuName = "ScriptableObjet/LevelData" , order = 2)]
    public class LevelData : ScriptableObject
    {
        public int initialGold;
        public int intialLife;
        public WaveData[] waves;
        public AudioClip levelMusic;
    }
}
