using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    private Rigidbody rb;
    private readonly string playertag = "Player";
    private readonly string jumpTag = "JUMPSUPORT";
    private readonly string bulletTag = "BULLET";
    private readonly int hashJump = Animator.StringToHash("IsJump_T");
    private readonly int hashHit = Animator.StringToHash("IsHit_T");
    private readonly int hashDie = Animator.StringToHash("IsDie_T");
    private bool isJumping = false;
    private Animator anim;
    private NavMeshAgent agent;
    private int hp;
    private int maxHp = 100;
    public bool isDie = false;
  

    [Header("HpUI")]
    public Image hpBar;
    public Text hpText;
    public Canvas canvas;
    void Start()
    {
        hpBar.color = Color.green;
        canvas = GetComponentInChildren<Canvas>();
        hp = maxHp;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isJumping && agent.isOnOffMeshLink)
        {
            StartCoroutine(EnemyJump()); // 점프 코루틴 시작
        }
    }
    private void OnCollisionEnter(Collision col) // 콜백 함수 스스로 호출된다
    {
       if(col.gameObject.CompareTag(playertag))
        {
            this.rb.mass = 1000f;
            rb.isKinematic = true; // 물리 효과 해제
        }
       else if(col.gameObject.CompareTag(bulletTag))
        {
            anim.SetTrigger(hashHit);
            Destroy(col.gameObject);
            hp -= 25;
            hp = Mathf.Clamp(hp, 0, maxHp);
            hpBar.fillAmount = (float)hp / maxHp;
            if (hpBar.fillAmount <= 0.3f) 
                hpBar.color = Color.red;
            else if(hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow;
            hpText.text = $"HP : <color=#f00> {hp}</color>";
            
        }
       if(hp <= 0)
        {
            Die();
        }
    }
    private void OnCollisionExit(Collision col) // 콜백 함수 스스로 호출된다
    {
        if (col.gameObject.CompareTag(playertag))
        {
            this.rb.mass = 65f;
            rb.isKinematic = false; // 물리 효과를 준다
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(jumpTag) && isJumping == false)
        {
            isJumping = true;
            anim.SetTrigger(hashJump);
            agent.speed = 0.1f;
        }
    }
   

    void Die()
    {
        isDie = true;
        anim.SetTrigger(hashDie);
        Destroy(gameObject, 5f);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = false;
        canvas.enabled = false;
        GameManager.instance.UpdateKillCount(1); // 게임 매니저의 총 킬 카운트 증가
    }
    IEnumerator EnemyJump()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        isJumping = false;
        agent.speed = 3.5f;
    }
 
}
