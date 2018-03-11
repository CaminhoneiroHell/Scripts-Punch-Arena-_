using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class GameFocus : MonoBehaviour {
    private float thickTime = 3;
    private float lastThick;
    public bool autoFocus = true;
    public bool isOnFocus;

    public void Focus() {
        if (!isOnFocus) {
            string path = Util.batUtilPath + "/GameFocus.vbs";
            try {
                Process.Start(path);
                UnityEngine.Debug.Log("Application Focused");
            }
            catch (System.Exception e){
                UnityEngine.Debug.Log("Could not focus application: " + e);
            }
        }
    }

    private void Update() {
        if (autoFocus && !isOnFocus) {
            if (lastThick + thickTime < Time.time) {
                lastThick = Time.time;
                Focus();
            }
        }
    }

    private void OnApplicationFocus(bool focus) {
        isOnFocus = focus;
        if (autoFocus && !isOnFocus) {
            Focus();
        }
    }
}