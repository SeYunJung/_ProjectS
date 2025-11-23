using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint; // 일꾼 스폰 지점 
    [SerializeField] private GameObject _workerPrefab; // 일꾼 프리팹
    //[SerializeField] private float _spawnTime; // 일꾼 스폰 주기
    [SerializeField] private Transform[] _targetPoints; // 일꾼 이동 경로
    private int _monsterCount; // 일꾼 수 
    private bool _flag;
    public List<Worker> workerList { get; private set; }

    private GameManager _gameManager;

    public void Init()
    {
        _gameManager = GameManager.instance;
        workerList = new List<Worker>();

        SpawnWorker();
    }

    public void SpawnWorker()
    {
        GameObject spawnWorker = Instantiate(_workerPrefab, _spawnPoint);
        Worker worker = spawnWorker.GetComponent<Worker>();

        if(workerList.Count < 20)
        {
            workerList.Add(worker);
            worker.Init(!_flag, _targetPoints);
            _flag = !_flag;
        }
        else
        {
            Debug.Log("일꾼은 20명까지 소환 가능");
        }
    }
}
