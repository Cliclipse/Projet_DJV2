using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSession" , menuName = "ScriptableObjet/GameSession" , order = 0)]
    public class GameSession : ScriptableObject
    {
        public int levelIndex;
    }
}