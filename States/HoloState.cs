using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HoloState : MonoBehaviour, IFSMState
{
    GameObject changeEffect;
    GameObject spaceStation, tripod, city;
    Camera ovrCamera;

    public IEnumerator Enter()
    {
        yield return SceneManager.LoadSceneAsync("HoloState");
        StartCoroutine(HoloFlux());

        //Pega componentes
        changeEffect = GameObject.Find("HologramChangeEffect");
        spaceStation = GameObject.Find("SpaceStationHologram");
        tripod = GameObject.Find("tripodHologram");
        city = GameObject.Find("HoloCityArchtecture");
        ovrCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        //Desativa componentes
        changeEffect.SetActive(false);
        spaceStation.SetActive(false);
        tripod.SetActive(false);
        city.SetActive(false);

        //Inicia eventos da cena
        StartCoroutine(HoloFlux());
    }

    IEnumerator HoloFlux()
    {
        yield return new WaitForSeconds(0.2f);
        if (tripod != null)
            tripod.SetActive(true);

        yield return new WaitForSeconds(10f);
        //Instantiate(changeEffect, transform.position, transform.rotation);
        changeEffect.SetActive(true);
        Destroy(tripod, 0.1f);

        yield return new WaitForSeconds(0.1f);
        if (spaceStation != null)
            spaceStation.SetActive(true);

        yield return new WaitForSeconds(10f);
        //Instantiate(changeEffect, transform.position, transform.rotation);
        changeEffect.SetActive(false);
        changeEffect.SetActive(true);
        Destroy(spaceStation, 0.1f);

        yield return new WaitForSeconds(0.1f);
        if (city != null)
            city.SetActive(true);

        yield return new WaitForSeconds(10f);
        //ovrCamera.GetComponent<OVRScreenFade>().FadeOutCall();
        yield return new WaitForSeconds(1.7f);
        GameStateManager.instance.ChangeState(State.Credits);

    }

    public IEnumerator Exit()
    {
        yield return new WaitForEndOfFrame();
    }

    public void FSMUpdate()
    {

    }
}
