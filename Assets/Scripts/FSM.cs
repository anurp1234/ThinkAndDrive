using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    List<IState> states;

    // Start is called before the first frame update
    void Start()
    {
        states = new List<IState>();
    }

    // Update is called once per frame
    void Update()
    {
        int count = states.Count;
        for (int i = 0; i < count; i++)
        {
            states[i].UpdateState();
        }
    }

    public void Push(IState state)
    {
        states.Add(state);
        state.SetupState();
    }

    public void Pop(IState state)
    {
        states.Remove(state);
        state.DestroyState();
    }
}
