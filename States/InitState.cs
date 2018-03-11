using UnityEngine;
using System.Collections;

public class InitState : MonoBehaviour, IFSMState
{

    //Enter in State
    public IEnumerator Enter()
    {
        yield return new WaitForEndOfFrame();
        GameStateManager.instance.ChangeState(State.ConfigState);
    }
    //Leave State
    public IEnumerator Exit()
    {
        yield return new WaitForEndOfFrame();
    }
    //Every Frame
    public void FSMUpdate(){
    }
}