using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticsState : MonoBehaviour, IFSMState
{
    [HideInInspector]
    GameObject m_Cam;

    public IEnumerator Enter()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadSceneAsync(2);
        GameStateManager.instance.statiscsController.GetComponent<StatisticsController>().CheckHighScore();
    }

    public IEnumerator Exit()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(1);
    }

    public void FSMUpdate()
    {
        //throw new NotImplementedException();
        //Inputs
        if (Input.GetKeyDown(KeyCode.Escape))
            ConfigState();
    }

    private void ConfigState(){
        GameStateManager.instance.ChangeState(State.ConfigState);
    }

}
