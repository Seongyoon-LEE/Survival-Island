using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.총알에 세번 맞으면 베럴 폭파 효과 구현 2.폭파 이펙트, 폭파 사운드 3.물리적인 폭파를 리지드바디를 이용해서 구현 
public class BarrelCtrl : MonoBehaviour
{
    [Header("Barrel Explosion 관련")]
    [SerializeField] GameObject explosionEffect; // 폭파 이펙트 프리팹
    [SerializeField] AudioClip explosionClip; // 폭파 사운드 클립
    [SerializeField] AudioSource source; // 폭파 사운드 오디오 소스
    [SerializeField] Rigidbody rb; // 베럴의 리지드바디 컴포넌트
    [SerializeField] Texture[] textures; // 베럴의 텍스쳐
    [SerializeField] MeshRenderer meshRenderer; // 베럴의 메쉬 렌더러 컴포넌트
    [SerializeField] Mesh[] meshes; // 베럴의 메쉬 배열
    [SerializeField] MeshFilter meshFilter; // 베럴의 메쉬 필터 컴포넌트
    [SerializeField] int hitCount = 0; // 총알 맞은 횟수
    [SerializeField] float radiuse = 20f; // 폭파 힘
    
    private readonly string bulletTag = "BULLET"; // 총알 오브젝트의 태그

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 리지드바디 컴포넌트 가져오기
        source = GetComponent<AudioSource>(); // 오디오 소스 컴포넌트 가져오기
        meshRenderer = GetComponent<MeshRenderer>(); // 메쉬 렌더러 컴포넌트 가져오기
        textures = Resources.LoadAll<Texture>("Textures1"); // Resources 폴더에서 베럴 텍스쳐 배열 로드
        meshRenderer.material.mainTexture = textures[Random.Range(0, textures.Length)]; // 랜덤으로 텍스쳐 설정
        meshFilter = GetComponent<MeshFilter>(); // 메쉬 필터 컴포넌트 가져오기
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag(bulletTag))
        {
            if(++hitCount == 3) // 총알 맞은 횟수가 3 이상이면
            {
                Explode(); // 폭파 함수 호출
            }
        }
    }
    void Explode()
    {
        if(explosionEffect != null)
        {
          var exp = Instantiate(explosionEffect, transform.position, Quaternion.identity); // 폭파 이펙트 생성
            Destroy(exp, 1.5f); // 2초 후에 폭파 이펙트 제거
            source.PlayOneShot(explosionClip, 3f); // 폭파 사운드 재생
            int idx = Random.Range(0, meshes.Length); // 랜덤으로 메쉬 인덱스 선택
            meshFilter.sharedMesh = meshes[idx]; // 메쉬 필터에 찌그러진 메쉬 랜덤 적용
            Collider[] colls = Physics.OverlapSphere(transform.position, radiuse, 1 << 13); // 베럴위치에서 20반경에 있는베럴 충돌체를 colls 배열에 하나씩 넣는다.
            foreach (Collider coll in colls)
            {
                Rigidbody _rb = coll.GetComponent<Rigidbody>(); // 충돌체의 리지드바디 컴포넌트 가져오기

                if (_rb != null)
                {
                    _rb.mass = 1.0f; // 리지드바디의 질량을 1로 변경
                    _rb.AddExplosionForce(800f, transform.position, radiuse, 300f); // 폭파 힘을 적용
                }                      // 폭파력,폭파 위치, 폭파 반경, 폭파 높이
            }
        }
    }
}
