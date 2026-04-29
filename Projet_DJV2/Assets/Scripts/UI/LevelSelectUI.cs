using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class SelectLevelUI : MonoBehaviour
    {
        [SerializeField] private GameSession gameSession;
        
        public void LoadLevel(int levelIndex)
        {
            gameSession.levelIndex = levelIndex;
            // Chargement de la scène
        }
    }
}
