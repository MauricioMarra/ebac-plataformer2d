using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class GameManager : Singleton<GameManager>
{
    private StateBase _currentState;
    private Dictionary<States, StateBase> _statesDictionary = new Dictionary<States, StateBase>();

    public MenuManager menuManager;
    public GameObject EnemyGroup;
    public SOCollectables enemyCount;

    private int _enemyCount = 0;

    public UnityEvent unityEventOnEnemyCountChange;

    private void Update()
    {

    }

    override public void Awake()
    {
        base.Awake();

        _statesDictionary.Add(States.Running, new StateRunning());
        _statesDictionary.Add(States.Paused, new StatePaused());
        _statesDictionary.Add(States.Death, new StateDeath());

        SwitchState(States.Running);

        if (unityEventOnEnemyCountChange == null)
            unityEventOnEnemyCountChange = new();
    }

    private void Start()
    {
        _enemyCount = EnemyGroup.GetComponentsInChildren<HealthBase>().Count();
        enemyCount.value = _enemyCount;

        unityEventOnEnemyCountChange.Invoke();
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

    public void UpdateEnemyCount()
    {
        this._enemyCount--;
        enemyCount.value = _enemyCount;

        unityEventOnEnemyCountChange.Invoke();
    }
}

public enum States
{
    Running,
    Paused,
    Death
}
