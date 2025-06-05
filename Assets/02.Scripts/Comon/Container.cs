using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.총알에 맞았을 때 맞은 위치에 이펙트 생성
//2.총알에 맞았을때 총알은 사라지고 맞은 위치에 자국 생성
public class Container : MonoBehaviour
{
    private readonly string bulletTag = "BULLET";
    public GameObject hitEffectPrefab;
    public AudioSource source;
    public AudioClip hitSound;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(bulletTag))
        {
            Destroy(col.gameObject); // 맞은 총알 파괴
            var hitEff = Instantiate(hitEffectPrefab, /*col.contacts[0].point*/ col.transform.position, Quaternion.identity);
            Destroy(hitEff, 1.5f); // 이펙트는 1.5초후에 파괴
            source.PlayOneShot(hitSound, 1.0f); // 맞았을때 사운드 재생
        }
    }

    void Update()
    {
        
    }
}
