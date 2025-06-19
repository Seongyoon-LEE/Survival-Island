using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.총알 충돌 감지후 총알은 사라짐 2.이펙트 효과 3.사운드 효과 
public class RemoveBullet : MonoBehaviour
{
    [SerializeField] private GameObject spark; // 총알이 충돌했을 때 생성할 이펙트 프리팹
    [SerializeField] private AudioSource source; // 총알 충돌 사운드를 재생할 오디오 소스
    [SerializeField] private AudioClip hitSound; // 총알 충돌 사운드
    
    private readonly string bulletTag = "BULLET"; // 총알 오브젝트의 태그
    void Start()
    {
        source = GetComponent<AudioSource>(); // 오디오 소스 컴포넌트 가져오기
        hitSound = Resources.Load("Sounds/bullet_hit_metal_enemy_4") as AudioClip; // Resources 폴더에서 총알 충돌 사운드 로드
        spark = Resources.Load("Effects/FlareMobile") as GameObject; // Resources 폴더에서 이펙트 프리팹 로드
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == bulletTag)
        {
            //Destroy(col.gameObject); // 충돌한 총알 오브젝트 제거
            col.gameObject.SetActive(false); // 충돌한 총알 오브젝트 비활성화 (풀링을 위해)
            source.PlayOneShot(hitSound, 0.5f); // 총알 충돌 사운드 재생
            ContactPoint contact = col.contacts[0]; // 첫번째 충돌 지점 정보를 ContactPoint 구조체에 전달

            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal); // 법선벡터가 이루는 회전 각도 추출
            var spk = Instantiate(spark, contact.point, rot); // 충돌 지점에 이펙트 생성
            Destroy(spk, 1f); // 1초 후에 이펙트 오브젝트 제거
        }
    }
}
