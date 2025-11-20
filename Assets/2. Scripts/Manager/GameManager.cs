using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MonsterSpawnManager monsterSpawnManager {  get; private set; }
    public ObjectPool objectPool { get; private set; }

    [SerializeField] private int _waveNumber;

    [field:SerializeField] public Player player {  get; set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        monsterSpawnManager = GetComponentInChildren<MonsterSpawnManager>();
        objectPool = GetComponentInChildren<ObjectPool>();

        GameStart();
    }

    private void GameStart()
    {
        // 스폰매니저 초기화 
        monsterSpawnManager.Init(this);

        // 플레이어 초기화
        player.Init();

        // 웨이브 시작 
        StartCoroutine(monsterSpawnManager.StartWave(_waveNumber));
    }
}