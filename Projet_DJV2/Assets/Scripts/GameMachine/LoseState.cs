using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseState : IState
{
    private readonly LevelController _levelController;

    public LoseState(LevelController levelController)
    {
        _levelController = levelController;
        //_levelController.LoseScreen.GetComponentInChildren<Button>().onClick.AddListener(OnRetry);
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _levelController.LoseScreen.gameObject.SetActive(true);
    }

    public void Execute()
    {
        Debug.Log("Entrée Etat Defaite");
        // per-frame logic, include condition to transition to a new state
    }

    public void Exit()
    {
        Debug.Log("Sortie Etat Defaite");
        Time.timeScale = 1;

        //_levelController.LoseScreen.gameObject.SetActive(false);
        //SceneManager.LoadScene(1);
    }

    private void OnRetry()
    {
        Debug.Log("Retry");
        
        //_levelController.GameStateMachine.TransitionTo(_levelController.GameStateMachine.PlayState);
    }
}
