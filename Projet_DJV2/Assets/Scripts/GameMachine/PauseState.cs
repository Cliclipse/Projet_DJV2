using Level;
using UnityEngine;

public class PauseState : IState
{
    private readonly LevelController _levelController;

    public PauseState(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void Enter()
    {
        Debug.Log("Entrée Etat pause");
        _levelController.PauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        _levelController.AddPauseListener(OnPause);
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _levelController.TogglePause();
        }
    }

    public void Exit()
    {
        Debug.Log("Sortie Etat pause");
        _levelController.PauseScreen.gameObject.SetActive(false);

        _levelController.RemovePauseListener(OnPause);
    }

    private void OnPause()
    {
        _levelController.GameStateMachine.TransitionTo(_levelController.GameStateMachine.PlayState);
    }
}
