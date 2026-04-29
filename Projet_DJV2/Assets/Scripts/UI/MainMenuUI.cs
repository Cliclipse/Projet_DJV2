using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quitting game");
        }
    }
}
