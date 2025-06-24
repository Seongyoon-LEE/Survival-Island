using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager p_Instance = null;
    [Header("Object Player Bullet Pool")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxPool = 10;
    [SerializeField] private List<GameObject> bulletPool = new List<GameObject>();

    [Header("Object Enemy Bullet Pool")]
    [SerializeField] private GameObject e_bulletPrefab;
    [SerializeField] private int e_maxPool = 20;
    [SerializeField] private List<GameObject> e_bulletPool = new List<GameObject>();

    [Header("Object Enemy Pool")]
    public GameObject enemyPrefab;
    public List<Transform> spawnList;
    public List<GameObject> enemyPool;

    
    private void Awake()
    {
        if (p_Instance == null)
        {
            p_Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (p_Instance != this)
        {
            Destroy(this.gameObject);
        }
        var spawnPos = GameObject.Find("SpawnPoints").gameObject;
        if(spawnPos != null)
            spawnPos.GetComponentsInChildren<Transform>(spawnList);
        spawnList.RemoveAt(0); // 첫번째는 자기 자신이므로 제거

        CreateBullet();
        Create_e_Bullet();
        StartCoroutine(CreateEnemyPooing());
      
    }

    private void Start()
    {
       
            InvokeRepeating("EnemySpawn", 0.02f, 3.0f); // StartCoroutin보다 약간 더 빠르다
        //else
        //    CancelInvoke("EnemySpawn");
    }
    IEnumerator CreateEnemyPooing()
    {
        yield return new WaitForSeconds(0.5f);
        var EnemyGroup = new GameObject("EnemyGroup");
        for(int i = 0; i <10; i++)
        {
            var enemy = Instantiate(enemyPrefab,EnemyGroup.transform);
            enemy.name = $"적 : {i + 1} 명";
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }
    public void EnemySpawn()
    {
        foreach(var _enemy in enemyPool)
        {
            //if (GameManager.instance.isGameOver) break; // 게임 오버시 적 생성 중지
            if (_enemy.activeSelf == false)
            {
                _enemy.transform.position = spawnList[Random.Range(0,spawnList.Count)].transform.position;
                _enemy.transform.rotation = spawnList[Random.Range(0,spawnList.Count)].transform.rotation;
                _enemy.gameObject.SetActive(true);
                break;
            }
        }
    }
    private void CreateBullet()
    {
        GameObject objectPools = new GameObject("ObjectPoolsPlayer");
        for (int i = 0; i < maxPool; i++)
        {
            var bullet = Instantiate(bulletPrefab, objectPools.transform);
            bullet.name = $"총알 : {i + 1} 발";
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        
    }

    private void Create_e_Bullet()
    {
        GameObject e_objectPools = new GameObject("ObjectPoolsEnemy");
        for (int i = 0; i < e_maxPool; i++)
        {
            var e_bullet = Instantiate(e_bulletPrefab, e_objectPools.transform);
            e_bullet.name = $"적 총알 : {i + 1} 발";
            e_bullet.SetActive(false);
            e_bulletPool.Add(e_bullet);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0;i < bulletPool.Count;i++)
        {
            if (bulletPool[i].activeSelf == false) // 활성화인지 비활성화 인지 자동체크
            {
                return bulletPool[i]; // 비활성화 된 것만 반환
            }
        }
        return null;
    }

    public GameObject Get_e_Bullet()
    {
        for (int i = 0; i < e_bulletPool.Count; i++)
        {
            if (e_bulletPool[i].activeSelf == false) // 활성화인지 비활성화 인지 자동체크
            {
                return e_bulletPool[i]; // 비활성화 된 것만 반환
            }
        }
        return null;
    }
}
