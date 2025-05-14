using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.�ݶ��̴��� �ε����� �� ����Ʈ�� �������� ����
// 2.�ݶ��̴� �ۿ� ������ ����Ʈ�� �������� ����

public class LightOnOff : MonoBehaviour
{
    public Light _light;
    private AudioSource source;
    public AudioClip _lighteOnSound;
    public AudioClip _lighteOffSound;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) // isTriggerüũ �ǰ� �ݶ��̴� �ȿ� ������ ��
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _light.enabled = true;
            source.PlayOneShot(_lighteOnSound);
        }
    }
    private void OnTriggerExit(Collider other) // isTriggerüũ �ǰ� �ݶ��̴� �ۿ� ������
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _light.enabled = false;
            source.PlayOneShot(_lighteOffSound);
        }
    }
}
