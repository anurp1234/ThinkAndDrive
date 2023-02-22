using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void SetupState();
    void UpdateState();
    void DestroyState();
}
