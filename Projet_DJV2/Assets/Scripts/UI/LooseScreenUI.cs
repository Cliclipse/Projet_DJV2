using System;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LooseScreenUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private TextMeshProUGUI waveTMP;

        private LevelController _levelController;
        
        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();    
        }

        void Update()
        {
            scoreTMP.text = $"Score : {_levelController.ScoreController.Score}";
            waveTMP.text = $"Wave {_levelController.WaveNumber} / {_levelController.WaveCount}";
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
