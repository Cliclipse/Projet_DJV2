using Level;
using UnityEngine;
using UnityEngine.Events;

namespace GameMachine
{
    public class GameStateMachine
    {
        public IState CurrentState { get; private set; }

        public PauseState PauseState;
        public PlayState PlayState;
        public WinState WinState;
        public LoseState LoseState;

        private UnityEvent<IState> _onStateChanged = new();

        public GameStateMachine(LevelController levelController)
        {
            Debug.Log("gameStatMachine creation");
            PauseState = new PauseState(levelController);
            PlayState = new PlayState(levelController);
            WinState = new WinState(levelController);
            LoseState = new LoseState(levelController);
        }

        public void AddStateChangedListener(UnityAction<IState> listener) => _onStateChanged.AddListener(listener);
        public void RemoveStateChangedListener(UnityAction<IState> listener) => _onStateChanged.RemoveListener(listener);

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            _onStateChanged.Invoke(state);
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            _onStateChanged.Invoke(nextState);
        }

        public void Update()
        {
            CurrentState?.Execute();
        }
    }
}
