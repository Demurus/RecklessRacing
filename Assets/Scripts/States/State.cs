using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    private static State _saved = null;

    public static State CurrentState;
    public static bool IsPause { get; private set; }
    public static event Action OnStateChanged;

    protected virtual void OnStateStart()
    {

    }

    protected virtual void OnStateEnd()
    {

    }
    protected virtual void OnStateRestart()
    {

    }
    protected virtual void OnStatePause()
    {

    }

    protected virtual void OnStateResume()
    {

    }

    public static void Start<T>(Hashtable param = null) where T : State, new()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateEnd();
        }
          
        CurrentState = new T();
        if (param != null)
            CurrentState.SetParameters(param);
        CurrentState.OnStateStart();
        OnStateChanged?.Invoke();
    }

    public virtual void SetParameters(Hashtable stateParams)
    {
    }

    public static void Pause<T>(Hashtable param = null) where T : State, new()
    {
        if (CurrentState != null)
        {
            _saved = CurrentState;
            _saved.OnStatePause();
            IsPause = true;
            CurrentState = null;
        }
        Start<T>(param);
    }

    public static void Resume()
    {
        if (_saved != null)
        {
            CurrentState = _saved;
            _saved = null;
            CurrentState.OnStateResume();
        }

        IsPause = false;
    }

    public static void OnGUIDebugHelper()
    {
        string msg = State.CurrentState == null ? "No State" : State.CurrentState.ToString();
        GUI.Button(new Rect(0, 0, 300, 100), msg, new GUIStyle() { fontSize = 40 });
    }
}
