using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.FŰ�� ������ ����Ʈ�� ������.
// 2.�ٽ� FŰ�� ������ ����Ʈ�� ������.
public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip LightOnclip;
    void Start()
    {
        flashLight = GetComponent<Light>();
        source = GetComponent<AudioSource>();
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLight.enabled = !flashLight.enabled;
            source.PlayOneShot(LightOnclip);
        }
    }
}
