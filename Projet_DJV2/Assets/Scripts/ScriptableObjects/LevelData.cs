using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData" , menuName = "ScriptableObjet/LevelData" , order = 2)]
    public class LevelData : ScriptableObject
    {
        public int levelIndex;
        public int initialGold;
        public int intialLife;
        public TextAsset waves;
        public int waveCount;
        public AudioClip levelMusic;
    }
}
