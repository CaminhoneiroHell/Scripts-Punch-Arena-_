using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameState : MonoBehaviour, IFSMState
{
    float walkIntoSceneSpeed = -5;
    
    float rotationToLookAtPlayer = -20;
    
    float walkToPlayerPosSpeed = -5;
    
    float animWalkSpeed = 1.0f;
    
    Animator anim;
    
    GameObject laserBeam;

    GameObject meteor;

    GameObject earthErosion;
    
    GameObject camera;
    
    GameObject tripod;

    AudioSource audio;

    bool fadeAudio;


    public IEnumerator Enter()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("GamesState");

        //Pega componentes
        audio = GameObject.Find("hips").GetComponent<AudioSource>();
        //tripod = GameObject.Find("tripod");
        anim = GameObject.Find("tripod").GetComponent<Animator>();
        laserBeam = GameObject.Find("LaserBeam");
        //meteor2 = GameObject.Find("Meteor (1)");
        meteor = GameObject.Find("Meteor");
        earthErosion = GameObject.Find("EarthErosion");
        camera = GameObject.Find("CenterEyeAnchor");

        //Desativa componentes
        //laserBeam.SetActive(false);
        //meteor2.SetActive(false);
        meteor.SetActive(false);
        //earthErosion.SetActive(false);
    }

    public IEnumerator Exit()
    {
        StopCoroutine(TripodBehaviour());
        yield return new WaitForEndOfFrame();
    }

    public void FSMUpdate()
    {
        StartCoroutine(TripodBehaviour());
        if (fadeAudio)
            audio.volume -= 0.5f * Time.deltaTime;
    }


    IEnumerator TripodBehaviour()
    {
        if (laserBeam != null)
            laserBeam.SetActive(true);

        //meteor2.SetActive(true);
        //anim.SetFloat("speed", animWalkSpeed);
        yield return new WaitForSeconds(6f);
        walkIntoSceneSpeed = 0;
        //if (laserBeam != null)
        //    laserBeam.SetActive(true);
        //if (earthErosion != null)
        //    earthErosion.SetActive(true);

        yield return new WaitForSeconds(4f);
        //if(meteor != null)
        //    meteor.SetActive(true);
        Destroy(laserBeam, 0.1f);
        rotationToLookAtPlayer = 0;

        yield return new WaitForSeconds(1f);
        if (meteor != null)
            meteor.SetActive(true);

        yield return new WaitForSeconds(4.5f);
        if(camera != null)
            camera.GetComponent<Animator>().SetBool("Glitch", true);

        fadeAudio = true;

        yield return new WaitForSeconds(0.2f);
        GameStateManager.instance.ChangeState(State.SeriousGame);

    }
}
