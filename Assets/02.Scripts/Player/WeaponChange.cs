using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//1. 메쉬 렌더러가 필요
public class WeaponChange : MonoBehaviour
{
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] M4A1;
    public MeshRenderer[] UMP;
    public Animation anim;
    public bool isHaveM4A1 = false;
    public bool isHaveUMP = false;

    private readonly string weaponAniName = "draw"; 

    void Start()
    {
        anim = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
    }

    void Update()
    {                           //키보드 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponAni();
            KeyOne();
            isHaveM4A1 = true;
            isHaveUMP = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponAni();
            KeyTwo();
            isHaveM4A1 = false;
            isHaveUMP = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponAni();
            KeyThree();
            isHaveM4A1 = false;
            isHaveUMP = false;
        }
    }

    private void KeyThree()
    {
        spas12.enabled = true; // spas12 활성화
        for (int i = 0; i < UMP.Length; i++)
            UMP[i].enabled = false;
        for (int i = 0; i < M4A1.Length; i++)
            M4A1[i].enabled = false;
    }

    private void KeyTwo()
    {
        for (int i = 0; i < UMP.Length; i++)
        {
            UMP[i].enabled = true; // UMP 활성화
        }
        spas12.enabled = false;
        for (int i = 0; i < M4A1.Length; i++)
        {
            M4A1[i].enabled = false;
        }
    }

    private void KeyOne()
    {
        for (int i = 0; i < M4A1.Length; i++)
        {
            M4A1[i].enabled = true; // M4A1 활성화
        }
        spas12.enabled = false;

        for (int i = 0; i < UMP.Length; i++)
        {
            UMP[i].enabled = false;
        }
    }

    void WeaponAni()
    {
        anim.Play(weaponAniName); 
    }

}
