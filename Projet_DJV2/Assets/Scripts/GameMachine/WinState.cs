using Level;
using UnityEngine;
using UnityEngine.UI;

public class WinState : IState
{
    private readonly LevelController _levelController;

    public WinState(LevelController levelController)
    {
        _levelController = levelController;
        //_levelController.WinScreen.GetComponentInChildren<Button>().onClick.AddListener(OnNextLevel);
    }

    public void Enter()
    {
        Time.timeScale = 0;
        
        //_levelController.WinScreen.gameObject.SetActive(true);
    }

    public void Execute()
    {
        // per-frame logic, include condition to transition to a new state
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Debug.Log("ExitWinState");
        //_levelController.WinScreen.gameObject.SetActive(false);
        //_levelController.LaunchNextArena();
    }

    private void OnNextLevel()
    {
        _levelController.GameStateMachine.TransitionTo(_levelController.GameStateMachine.PlayState);
    }
}
