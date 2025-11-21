using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MonsterSpawnManager monsterSpawnManager {  get; private set; }
    public ObjectPool objectPool { get; private set; }
    public UIManager uiManager { get; private set; }

    private int _currentWaveNumber;

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

        GameStart();
    }

    private void GameStart()
    {
        // 스폰매니저 초기화 
        monsterSpawnManager.Init(this);

        // 플레이어 초기화
        player.Init();

        // 웨이브 시작 
        StartCoroutine(monsterSpawnManager.WaveStart());
    }

    public void EndGame()
    {
        StopAllCoroutines();
        uiManager.ActiveResultPanel(_currentWaveNumber);
    }

    public bool isEndGame()
    {
        return _currentWaveNumber == Constants.FinalWave;
    }

    public void UpdateCurrentWaveNumber(int waveNumber)
    {
        _currentWaveNumber = waveNumber;
    }
}