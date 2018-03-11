using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ConfigState : MonoBehaviour, IFSMState {

    bool fadeMusic;
    public AudioSource music;
    public IEnumerator Enter()
    {
        SceneManager.LoadSceneAsync(1);
        yield return new WaitForSeconds(3f);

        //Get music reference
        try{
            music = GameObject.Find("SFX").GetComponent<AudioSource>();
        }
        catch{
            print("Dont get music");
        }

        //Proximity sensor user check
        FindObjectOfType<ProximitySensor>().OnUserMountHeadset += () =>
        {
            GameStateManager.instance.cameraObj.GetComponent<CamEfxManager>().playerReady = true;
            StartCoroutine(EnterGameRoutine()); //Initialize rotine for entering the game
                                                //GameStateManager.instance.ChangeState(State.MultiPlayerState);
            print("Mounted Headset!");
        };

        FindObjectOfType<ProximitySensor>().OnUserUnmountHeadset += () =>
        {
            GameStateManager.instance.cameraObj.GetComponent<CamEfxManager>().playerReady = false;
            StopCoroutine(EnterGameRoutine());//Cancel rotine for entering the game
                                              //GameStateManager.instance.gameFocus.autoFocus = true; //Turn on focus on system
            print("Unmounted headset!");
        };
        yield return new WaitForSeconds(10f);
        fadeMusic = true;
        yield return new WaitForSeconds(2f);

    }
    private IEnumerator EnterGameRoutine()
    {
        yield return new WaitForSeconds(3f);
        RegisterBattleOnStatistics();
        EnterGame();
        //EnterCalibration();

    }

    /// <summary>
    /// Start game by Bat
    /// </summary>
    public static void EnterGame()
    {
        string path = Util.gameBatPath;//.SetActive(false);
        try
        {
            Process.Start(path);
            UnityEngine.Debug.Log("Entering game ");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log("Cannot open Game " + " error: " + e.Message);
        }
    }

    /// <summary>
    /// Calibration mode/// NOT BEING USED ///
    /// 
    /// </summary>

    public static void EnterCalibration()
    {
        string path = Util.calibrationBatPath;//.SetActive(false);
        try
        {
            Process.Start(path);
            UnityEngine.Debug.Log("Entering calibration ");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log("Cannot open Game " + " error: " + e.Message);
        }
    }



    public IEnumerator Exit()
    {
        yield return new WaitForEndOfFrame();
    }

    public static void RegisterBattleOnStatistics()
    {
        GameStateManager.instance.statiscsController.GetComponent<StatisticsController>().AddBattlesAndSave(SumScore.HighScore + 1);
    }

    public void FSMUpdate()
    {
        if (fadeMusic && music != null)
            music.volume += 0.5f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F1))
            GameStateManager.instance.ChangeState(State.StatisticsState);

        if (Input.GetKeyDown(KeyCode.F2))
            CalibrationSceneAndQuitGame();
    }

    void CalibrationSceneAndQuitGame()
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
}
