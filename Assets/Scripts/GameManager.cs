using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    private StateBase _currentState;
    private Dictionary<States, StateBase> _statesDictionary = new Dictionary<States, StateBase>();

    public MenuManager menuManager;

    override public void Awake()
    {
        base.Awake();

        _statesDictionary.Add(States.Running, new StateRunning());
        _statesDictionary.Add(States.Paused, new StatePaused());

        SwitchState(States.Paused);
    }

    public void SwitchState(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = _statesDictionary[state];

        _currentState.OnStateEnter();
    }

    public StateBase GetCurrentState()
    {
        return _currentState;
    }
}

public enum States
{
    Running,
    Paused
}
