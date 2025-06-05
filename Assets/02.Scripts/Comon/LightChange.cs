using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Light whiteLight;
    public Light blueLight;
    public Light yellowLight;
    public AudioClip lightChangeClip;
    AudioSource lightSource;
    void Start()
    {
        //whiteLight = GameObject.Find("OutLights").transform.GetChild(0).GetComponent<Light>();
        //blueLight = GameObject.Find("OutLights").transform.GetChild(1).GetComponent<Light>();
        //yellowLight = GameObject.Find("OutLights").transform.GetChild(2).GetComponent<Light>();
        whiteLight = GetComponentsInChildren<Light>()[0];
        blueLight = GetComponentsInChildren<Light>()[1];
        yellowLight = GetComponentsInChildren<Light>()[2];
        lightSource = GetComponent<AudioSource>();
        TurnOnLight(); 
    }
    void TurnOnLight()
    {
        StartCoroutine(LightOnOff());
    }
    IEnumerator LightOnOff()
    {
        whiteLight.enabled = true;
        yellowLight.enabled = false;
        blueLight.enabled = false;
        lightSource.PlayOneShot(lightChangeClip);
        yield return new WaitForSeconds(3f);

        whiteLight.enabled = false;
        yellowLight.enabled = true;
        blueLight.enabled = false;
        lightSource.PlayOneShot(lightChangeClip);
        yield return new WaitForSeconds(3f);

        whiteLight.enabled = false;
        yellowLight.enabled = false;
        blueLight.enabled = true;
        lightSource.PlayOneShot(lightChangeClip);
        yield return new WaitForSeconds(3f);

        TurnOnLight();
    }

}
