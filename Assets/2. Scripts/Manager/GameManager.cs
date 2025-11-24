using UnityEngine;

public class GameManager : MonoBehaviour_Singleton<GameManager>
{
    public MonsterSpawnManager monsterSpawnManager {  get; private set; }
    public ObjectPool objectPool { get; private set; }
    public UIManager uiManager { get; private set; }
    public HeroSpawnManager heroSpawnManager { get; private set; }
    public WorkerSpawnManager workerSpawnManager { get; private set; }
    public DataManager dataManager { get; private set; }

    private int _currentWaveNumber;

    public Player player {  get; private set; }
    public Grid grid { get; private set; }

    private void Start()
    {
        monsterSpawnManager = GetComponentInChildren<MonsterSpawnManager>();
        objectPool = GetComponentInChildren<ObjectPool>();
        uiManager = GetComponentInChildren<UIManager>();
        heroSpawnManager = GetComponentInChildren<HeroSpawnManager>();
        workerSpawnManager = GetComponentInChildren<WorkerSpawnManager>();
        dataManager = DataManager.instance;

        GameStart();
    }

    private void GameStart()
    {
        dataManager.Init();

        // 플레이어 가져오기 
        player = dataManager.player;
        grid = dataManager.grid;

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