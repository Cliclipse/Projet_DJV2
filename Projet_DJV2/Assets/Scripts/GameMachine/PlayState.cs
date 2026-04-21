using Level;
using UnityEngine;

public class PlayState : IState
{
    private readonly LevelController _levelController;

    public PlayState(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void Enter()
    {
        Debug.Log("Enter PlayState");
        _levelController.AddPauseListener(OnPause);
        Time.timeScale = 1f;
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
        Debug.Log("Exit PlayState");
        _levelController.RemovePauseListener(OnPause);
    }

    private void OnPause()
    {
        _levelController.GameStateMachine.TransitionTo(_levelController.GameStateMachine.PauseState);
    }
}
