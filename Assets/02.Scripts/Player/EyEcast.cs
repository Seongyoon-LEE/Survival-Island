using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyEcast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Transform tr;
    private CrossHair crossHair; // CrossHair 스크립트 참조 

    void Start()
    {
        crossHair = GameObject.Find("Image-Aim").GetComponent<CrossHair>(); // CrossHair 오브젝트 찾기
        tr = transform;
    }
    void Update()
    {
        ray = new Ray(tr.position, tr.forward); // 현재 위치에서 앞 방향으로 레이 생성
        //Debug.DrawRay(tr.position, tr.forward * 20f, Color.red); // 레이 시각화
        if (Physics.Raycast(ray, out hit, 100f, 1 << 7 | 1 << 8))
        {
            crossHair.isGaze = true;
        }
        else
        {
            crossHair.isGaze = false;
        }
    }
}
