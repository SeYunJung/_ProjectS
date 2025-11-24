using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MonsterSpawnManager monsterSpawnManager {  get; private set; }
    public ObjectPool objectPool { get; private set; }
    public UIManager uiManager { get; private set; }
    public HeroSpawnManager heroSpawnManager { get; private set; }
    public WorkerSpawnManager workerSpawnManager { get; private set; }

    private int _currentWaveNumber;

    // 리펙토링 : 씬 전환되면 missing 남. 
    // -> 게임시작할 때 프리팹으로 생성되게 하자. 
    [field:SerializeField] public Player player {  get; set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        monsterSpawnManager = GetComponentInChildren<MonsterSpawnManager>();
        objectPool = GetComponentInChildren<ObjectPool>();
        uiManager = GetComponentInChildren<UIManager>();
        heroSpawnManager = GetComponentInChildren<HeroSpawnManager>();
        workerSpawnManager = GetComponentInChildren<WorkerSpawnManager>();

        GameStart();
    }

    private void GameStart()
    {
        // 매니저 초기화 
        uiManager.Init();
        monsterSpawnManager.Init(this);
        workerSpawnManager.Init();
        objectPool.Init();
        heroSpawnManager.Init();

        // 플레이어 초기화
        player.Init();

        // 웨이브 시작 
        StartCoroutine(monsterSpawnManager.WaveStart());
    }

    public void EndGame()
    {
        StopAllCoroutines();
        uiManager.ActiveUIResult(_currentWaveNumber);
    }

    public bool isEndGame()
    {
        return _currentWaveNumber == Wave.FINAL;
    }

    public void UpdateCurrentWaveNumber(int waveNumber)
    {
        _currentWaveNumber = waveNumber;
    }
}