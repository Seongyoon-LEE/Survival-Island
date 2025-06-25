using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//싱글톤 기법을 사용하여 게임 매니저를 구현합니다.
//적 태어나기 1. 태어날 위치 2. 태어날 시간 3. 태어날 적 종류를 설정합니다.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 기법
    //1. 무분별한 객체 생성 방지
    //2. 전역에서 쉽게 접근 가능 
    public GameObject zombiePrefab; // 좀비 프리팹  
    public GameObject skeletonPrefab; // 스켈레톤 프리팹
    public List<Transform> spawnList;

    [Header("Inventory 관련")]
    public GameObject inventory;
    private bool isInventoryOpen = false; // 상태 저장

    [Header("Pause 관련")]
    private bool isPaused = false;

    public Text killText; // UI에 표시할 킬수
    private float timePrev;
    private float timePrev2;
    private int maxZombieCount = 10; // 최대 좀비 수
    private int maxSkeletonCount = 5; // 최대 스켈레톤 수
    public int totalkill = 0; // 총 킬 카운트
    PlayerDamage playerDamage;

    [Header("Tag 관련")]
    readonly string playerTag = "Player";
    private readonly string zombieTag = "ZOMBIE";
    private readonly string skeletonTag = "SKELETON";

    private void Awake() // Start() 전에 호출되는 함수로, 싱글톤 패턴을 구현합니다.
    {
        if(Instance == null) // 싱글톤 인스턴스가 없으면 
        {
            Instance = this; // 현재 인스턴스를 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }
    }
    void Start()
    {
        MouseCursorVisible();
        playerDamage = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>();
        if (playerDamage.isPlayerDie) return;
        if(Instance != null)
        killText = GameObject.Find("Panel-Kill").transform.GetChild(0).GetComponent<Text>(); // UI에서 킬수 텍스트를 찾음
        else
            killText = null;

        timePrev = Time.time; // 좀비 생성 시간 초기화
        timePrev2 = Time.time; // 스켈레톤 생성 시간 초기화
        
  
            Transform[] spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>(); // 하이라키에서 SpawnPoints 오브젝트를 찾고 찾은 자식 트랜스폼을 가져옴
            if (spawnPoints != null)
                spawnList = new List<Transform>(spawnPoints); // SpawnPoints의 자식 트랜스폼을 리스트로 변환
            spawnList.RemoveAt(0); // 첫 번째 요소(부모 트랜스폼)를 제거하여 실제 스폰 위치만 남김
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDamage.isPlayerDie) return;

        if (Time.time - timePrev >= 3f) // 3초마다 좀비 생성
        {
            timePrev = Time.time; // 현재 시간을 이전 시간으로 설정
            int zombieCount = GameObject.FindGameObjectsWithTag(zombieTag).Length; // 현재 좀비 수를 계산, FindGameObjectsWithTag를 사용하여 "Zombie" 태그를 가진 모든 오브젝트를 찾음
            if (zombieCount < maxZombieCount) // 최대 좀비 수를 초과하면 생성하지 않음
                CreateZombie();
        }
        if(Time.time - timePrev2 >= 5f) // 5초마다 스켈레톤 생성
        {
            timePrev2 = Time.time; // 현재 시간을 이전 시간으로 설정
            int skeletonCount = GameObject.FindGameObjectsWithTag(skeletonTag).Length; // 현재 스켈레톤 수를 계산
            if(skeletonCount < maxSkeletonCount) // 최대 스켈레톤 수를 초과하면 생성하지 않음
                CreateSkeleton();
        }
        // 인벤토리 토글 키 감지
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory() // 토글 인벤토리 
    {
        isInventoryOpen = !isInventoryOpen;

        var canvasGroup = inventory.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = isInventoryOpen;
        canvasGroup.alpha = isInventoryOpen ? 1.0f : 0.0f;
        canvasGroup.interactable = isInventoryOpen;

        PlayStop(isInventoryOpen);
        MouseCursorVisible();
    }
    private void PlayStop(bool pause) // 게임 일시 정지
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0f : 1f;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            MonoBehaviour[] scripts = playerObj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                script.enabled = !isPaused;
        }

        //if (panel_Weapon != null)
        //{
        //    var canvasGroup = panel_Weapon.GetComponent<CanvasGroup>();
        //    canvasGroup.blocksRaycasts = !isPaused;
        //}
    }
    void CreateZombie()
    {
       
        int idx = Random.Range(0,spawnList.Count); // 랜덤한 인덱스 생성
        Instantiate(zombiePrefab, spawnList[idx].position, spawnList[idx].rotation);
        //프리팹 생성함수 (what, where, rotation)
    }
    void CreateSkeleton()
    {
     
        int idx = Random.Range(0, spawnList.Count); // 랜덤한 인덱스 생성
        Instantiate(skeletonPrefab, spawnList[idx].position, spawnList[idx].rotation);
    }
    public void OnPauseClick()
    {
        ToggleInventory(); // ESC 누르면 인벤 꺼질 수 있도록 재활용 가능
    }
    public void UpdateKillCount(int killCount)
    {
        totalkill += killCount; // 총 킬 카운트 업데이트   
        killText.text = $"Kill : <color=f00>{totalkill.ToString()}</color>"; // 킬수 UI 업데이트
    }
    public void MouseCursorDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MouseCursorVisible()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
