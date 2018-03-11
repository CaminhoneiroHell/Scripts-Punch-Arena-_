using System.Collections.Generic;
using DevConsole;
using UnityEngine;

public static class C {

	[Command]
    static void CloseConsole() {
        Debug.Log("--- Console Closed ---");
        Console.Close();    
    }

    [Command]
    static void CheckDateTime() {
        Debug.Log(System.DateTime.Now);
    }
    [Command]
    static void CheckIsAuthorizedToRun() {
        //Debug.Log(GameManager.Instance.authorization.authorized);
    }

    [Command]
    static void CheckIsOnFocus() {
        Debug.Log(GameStateManager.instance.gameFocus.isOnFocus);
    }

    [Command]
    static void Focus(bool val){
        GameStateManager.instance.gameFocus.autoFocus = val;
    }
    
    [Command]
    static void State_Statistics(){
        GameStateManager.instance.ChangeState(State.StatisticsState);
    }

    [Command]
    static void State_Init()
    {
        Debug.LogWarning("This is state is only for create the GameManager dont enter here");
        GameStateManager.instance.ChangeState(State.Init);
    }

    [Command]
    static void State_Config()
    {
        GameStateManager.instance.ChangeState(State.ConfigState);
    }

    ///*******************COMANDO PARA RESETAR BATALHAS! DESCOMENTE QUANDO PRECISO.*****************************************///

    [Command]
    static void XXX_ResetBattleRegister(bool reset)
    {
        if (reset)
            SumScore.ClearHighScore();

        Debug.LogError("AS BATALHAS FORAM RESETADAS (Na interface o total de batalhas está mostrando o total pela ultima vez fotografe ou printe esse total!  O VALOR SERÁ RESETADO COMPLETAMENTE NA PRÓXIMA INICIALIZAÇÃO)");
    }

    ///*******************COMANDO PARA RESETAR BATALHAS! DESCOMENTE QUANDO PRECISO.*****************************************///

    [Command]
    static void CalibrationScene()
    {
        string path = Util.calibrationBatPath;//.SetActive(false);
        try
        {
            System.Diagnostics.Process.Start(path);
            UnityEngine.Debug.Log("Entering calibration ");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log("Cannot open Game " + " error: " + e.Message);
        }
    }

    [Command]
    static void ProcessStart(string path) {
        try {
            System.Diagnostics.Process.Start(path);
        }
        catch (System.Exception e) {
            Debug.Log("error: " + e.Message);
        }
    }
}
