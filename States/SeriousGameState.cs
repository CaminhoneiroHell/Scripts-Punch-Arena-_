using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeriousGameState : MonoBehaviour, IFSMState
{
    private Camera camera;
    private GameObject canvans, finalCanvas;
    private GameObject satellite;
    private AudioSource audio;
    private Animator anim;
    bool moveCanvasaside, moveCameraToSpacialStation, moveCameraToUpperStation, camIsOnPosition, audioFade;

    public IEnumerator Enter()
    {
        yield return SceneManager.LoadSceneAsync("SeriousGameState");

        //Pega os componentes nescessários
        anim = GameObject.Find("SeriousGameStateManager").GetComponent<Animator>();
        canvans = GameObject.Find("Top");
        satellite = GameObject.Find("Low Poly Space Station");
        finalCanvas = GameObject.Find("PilgrimLogoAnimated");
        camera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        audio = GameObject.Find("Music").GetComponent<AudioSource>();

        anim.enabled = false;
        canvans.SetActive(false);
        finalCanvas.SetActive(false);
        satellite.SetActive(false);

        StartCoroutine(SeriousGameExplorationTour());
    }

    public IEnumerator Exit(){
        yield return new WaitForEndOfFrame();
    }

    public void FSMUpdate()
    {
        if(canvans != null){
            if (moveCanvasaside){
                canvans.GetComponent<RectTransform>().position += new Vector3(0, 2f * Time.deltaTime, 0);
                Destroy(canvans, 3f);
            }
        }
        if(audioFade)
            audio.volume -= 0.5f * Time.deltaTime;
    }

    IEnumerator SeriousGameExplorationTour(){
        yield return new WaitForSeconds(5f);
        camera.GetComponent<Animator>().SetBool("Glitch", true);
        yield return new WaitForSeconds(2f);
        canvans.SetActive(true);
        camera.GetComponent<Animator>().SetBool("Glitch", false);
        satellite.SetActive(true);
        yield return new WaitForSeconds(20f);
        moveCanvasaside = true;
        yield return new WaitForSeconds(2f);
        anim.enabled = true;
        anim.SetBool("Move", true);
        GameObject.Find("SeriousGameStateManager").GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(35f);
        finalCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        //camera.GetComponent<OVRScreenFade>().FadeOutCall();
        audioFade = true;
        yield return new WaitForSeconds(2f);
        GameStateManager.instance.ChangeState(State.Holo);
    }
}
