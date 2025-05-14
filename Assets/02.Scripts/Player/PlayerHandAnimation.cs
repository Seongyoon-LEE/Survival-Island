using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 1.Left Shift && W Ű�� ������ ���� ���� �ִϸ��̼��� ����ϴ� ��ũ��Ʈ
// 2.Left Shift || W ���� �ϳ��� ���� �ִϸ��̼��� ���߰� ���� �ܴ��� �ִϸ��̼��� ����ǵ��� ����  
public class PlayerHandAnimation : MonoBehaviour
{
    public Animation anim;
    private readonly string runAni = "running";
    private readonly string runStopAni = "runStop";
    private readonly string fireInput = "Fire1";
    private readonly string fireAni = "fire";

    private bool isRunning;

    void Start()
    {
        //anim = GetComponentInChildren<Animation>(); �ڽĵ��� �ִϸ��̼��� �������� ã�� ���� 
        //anim = GetComponentsInChildren<Animation>()[0]; �ڽĵ��� �迭�� �´� �ڽ��� �ִϸ��̼��� ������´�.

        //�ڱ� �ڽ��� ù��° �ڽ� ������Ʈ�� ã�� �� �ڽ��� ù��° ������Ʈ�� �ڽ� ������Ʈ�� ã�´�
        anim = transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        isRunning = false;
    }
    void Update()
    {
        PlayerRunAni();
        PlayerFire();
    }
    public void PlayerFire()
    {
        if (Input.GetButton(fireInput) && !isRunning)
        {
            anim.Play(fireAni);
        }
    }
    private void PlayerRunAni()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.Play(runAni);
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W))
        {
            anim.Play(runStopAni);
            isRunning = false;
        }
    }
}
