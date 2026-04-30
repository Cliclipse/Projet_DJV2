using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SelectLevelUI : MonoBehaviour
    {
        [SerializeField] private GameSession gameSession;
        
        public void LoadLevel(int levelIndex)
        {
            gameSession.levelIndex = levelIndex;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
