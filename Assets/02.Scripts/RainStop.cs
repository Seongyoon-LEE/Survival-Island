using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStop : MonoBehaviour
{
    public GameObject rainPrefab; // �� ������
    public GameObject rainObj; // �� ������Ʈ
    
    void Start()
    {
        rainObj = Instantiate(rainPrefab); // �� �������� �ν��Ͻ�ȭ �Ͽ� 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(rainObj); // �� ������Ʈ ����
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rainObj = Instantiate (rainPrefab); // �� �������� �ν��Ͻ�ȭ�Ͽ� rainObj�� ����
        }
    }

}
