using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

//마우스 왼쪽 클릭으로 총알을 발사하는 스크립트 
//뭐가 필요? 1.FirePos 2.총알 발사 프리팹 3. 오디오 소스 클립
public class FireBullet : MonoBehaviour
{
    [Header("Fire 관련")]
    public Transform FirePos; // 총알 발사 위치
    public GameObject BulletPrefab; // 총알 프리팹
    private AudioSource source;
    public AudioClip fireSound;
    public ParticleSystem muzzleFlash; // 총구 플래시 이펙트
    public ParticleSystem cartridgeEject; // 총구 플래시 이펙트

    [Header("Reload 관련")]
    public readonly float reloadTime = 1f; // 재장선 시간
    public readonly int maxBullet = 10; // 최대 총알 수
    public int currentBullet = 10; // 현재 총알 수 
    public bool isReloading = false; // 재장전중 여부    
    public Animation anim;

    [Header("스크립트 참조 관련")]
    WeaponChange weaponChange;
    PlayerHandAnimation playerHandAnimation;

    private float timePrev; // 과거시 만드려고

    void Start()
    {
        weaponChange = GetComponent<WeaponChange>();
        playerHandAnimation = GetComponent<PlayerHandAnimation>();
        muzzleFlash = GetComponentsInChildren<ParticleSystem>()[0];
        cartridgeEject = GetComponentsInChildren<ParticleSystem>()[1];
        anim = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        source = GetComponent<AudioSource>();
        timePrev = Time.time; 
    }

   
    void Update()
    {
        if(Input.GetMouseButton(0) && (weaponChange.isHaveM4A1 || weaponChange.isHaveUMP))
        {
            if ((Time.time - timePrev) > 0.1f)
            {
                Fire();
                timePrev = Time.time;
            }
        }

        else if(Input.GetMouseButtonDown(0))
        {
            Fire();
         
        }
        //else if(Input.GetMouseButtonUp(0))
        //{
        //    source.Stop(); // 오디오 소스 정지
        //    muzzleFlash.Stop();
        //    cartridgeEject.Stop();
        //}    
        
    }
    void Fire()
    {
        if (isReloading || playerHandAnimation.isRunning) return;
        Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
       
        source.PlayOneShot(fireSound,1.0f);
        cartridgeEject.Play(); // 총알 떨구는 이펙트 재생
        muzzleFlash.Play(); // 총구 플래시 이펙트 재생

        isReloading = (--currentBullet % maxBullet == 0); // 현재 총알수 감소후 재장전 여부 확인
        if(isReloading)
        {
            StartCoroutine(Reload()); // 재장전 코루틴 시작
        }
    }
    IEnumerator Reload()
    {
        anim.Play("pump2");
        yield return new WaitForSeconds(reloadTime); // 재장전 시간 대기
        currentBullet = maxBullet; 
        isReloading = false; // 재장전 여부를 false로 설정
    }
}
