using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsState : MonoBehaviour, IFSMState
{
    private Camera ovrCamera;

    public IEnumerator Enter(){
        yield return SceneManager.LoadSceneAsync("CreditsState");
        ovrCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        StartCoroutine(EndGame());
    }

    public IEnumerator Exit(){
        yield return new WaitForEndOfFrame();
    }

    public void FSMUpdate(){

    }

    IEnumerator EndGame(){
        yield return new WaitForSeconds(17f);
        //ovrCamera.GetComponent<OVRScreenFade>().enabled = true;
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
