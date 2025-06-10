using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//1.HP값 MAXHP값
//2.Image HPBar TEXT
//3.OnTriggerEnter
public class PlayerDamage : MonoBehaviour
{
    private float hp;
    private float maxHp = 100f; // 최대 채력
    public Image hpBar;
    public Text hpText;
    public GameObject bliendObj;
    public bool isPlayerDie = false;
    private string punchTag = "PUNCH";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(punchTag))
        {
            hp -= 10; // 체력감소
            hp = Mathf.Clamp(hp, 0, maxHp); // 체력을 0과 최대 체력 사이로 제한
            hpBar.fillAmount = hp / maxHp; // 체력바 UI 업데이트
            if(hpBar.fillAmount <= 0.3f)
                hpBar.color = Color.red;
            else if(hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow;
                hpText.text = $"HP : <color=#f00>{hp}</color>";
            if (hp <= 0)
                PlayerDie();
        }
        else if (other.gameObject.CompareTag("SWORD"))
        {
            hp -= 25;
            hp = Mathf.Clamp(hp, 0, maxHp); // 체력을 0과 최대 체력 사이로 제한
            hpBar.fillAmount = hp / maxHp; // 체력바 UI 업데이트
            if (hpBar.fillAmount <= 0.3f)
                hpBar.color = Color.red;
            else if (hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow;
            hpText.text = $"HP : <color=#f00>{hp}</color>";
            if (hp <= 0)
                PlayerDie();
        }
    }

    void Start()
    {
        hp = maxHp;
        hpBar = GameObject.Find("Canvas_UI").transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = GameObject.Find("Canvas_UI").transform.GetChild(0).GetChild(1).GetComponent<Text>();
        hpBar.color = Color.green;
        bliendObj = GameObject.Find("Canvas_UI").transform.GetChild(4).gameObject;
        
       
    }

    void PlayerDie()
    {
        bliendObj.SetActive(true);
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>(); 
        foreach(var script in scripts)
        {
            script.enabled = false; // 플레이어 스크립트 비활성화
        }
        Invoke("SceneMove", 3f);
        isPlayerDie = true;


    }
    void SceneMove()
    {
        SceneManager.LoadScene("EndScene");
    }
}
