using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBehaviour
{
    public void Enter();
    public void Update();
    public void Exit();
}
