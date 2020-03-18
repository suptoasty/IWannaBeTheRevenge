using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class StateMachine : MonoBehaviour {
    protected State State = null;
    protected bool _active = false;
    protected static Stack<GameObject> state_stack = new Stack<GameObject>();
    //find a "Stack" to do pushdown

    public void Start() {
        SetActive(true);
        State.Enter();
    }

    public void ChangeState(State state) {
        if(!_active) return;
        State.Exit();

        State = state;

        State.Enter();
    }

    public void SetActive(bool value) {
        _active = value;
        if(! _active) {
            state_stack = new Stack<GameObject>();
            State = null;
        }
    }

}