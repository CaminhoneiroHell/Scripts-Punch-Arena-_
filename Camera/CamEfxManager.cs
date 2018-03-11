using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEfxManager : MonoBehaviour {

    /// <summary>
    /// Set true when proximity sensor check true
    /// Do the effect of screen enter battle
    /// </summary>
    [Tooltip("Set true when proximity sensor check true / Do the effect of screen enter battle")]
    public bool playerReady = false;
    [SerializeField]
    private bool dontDestroyOnLoad = false;
    [SerializeField]
    private CameraFilterPack_AAA_BloodOnScreen bloodScreen;
    private void Awake()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    void Start () {
        bloodScreen = GetComponent<CameraFilterPack_AAA_BloodOnScreen>();
	}

    private void Update()
    {
        if (bloodScreen.Blood_On_Screen < 1.6f && playerReady)
            StartPunchArenaGame_Efx();
        if (bloodScreen.Blood_On_Screen > 0.7f && !playerReady)
            ReturnToPunchArenaSystem_Efx();
    }

    public void StartPunchArenaGame_Efx(){
         bloodScreen.Blood_On_Screen += 0.3f * Time.deltaTime;
    }

    public void ReturnToPunchArenaSystem_Efx(){
        bloodScreen.Blood_On_Screen -= 0.2f * Time.deltaTime;
    }

}
