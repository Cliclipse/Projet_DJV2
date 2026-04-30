using System;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WinScreenUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;

        private LevelController _levelController;
        
        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();    
        }

        void Update()
        {
            scoreTMP.text = $"Score : {_levelController.ScoreController.Score}";
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}
