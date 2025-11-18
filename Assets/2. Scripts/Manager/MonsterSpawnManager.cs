using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    /*
     
    적을 생성 

    적 생성에 필요한 정보
    - 스폰 지점
    - 적 오브젝트 (프리팹)
    
    적을 생성하는 방법
    - Instantiate


     */

    // 몬스터 변수
    [SerializeField] private Transform _spawnPoint; // 몬스터 스폰 지점 
    [SerializeField] private List<GameObject> _monsterPrefabList; // 몬스터 프리팹 리스트
    [SerializeField] private float _spawnTime; // 몬스터 스폰 주기
    [SerializeField] private Transform[] _targetPoints; // 몬스터 이동 경로 (여러 곳으로 이동할 수 있다)
    [SerializeField] private int[] _waveMonsterCount; // 웨이브별 몬스터 스폰 수 
    private int _monsterCount;
    public List<Monster> monsterList { get; private set; }

    private GameManager _gameManager;

    // 스폰할 몬스터들 저장 -> 필요없다. => 특정 몬스터를 지정해서 스폰할 필요가 없기 때문 
    //public void Init()
    //{
    //    _monsterDict = new Dictionary<string, GameObject>();

    //    foreach(GameObject monsterPrefab in _monsterPrefabList)
    //    {
    //        _monsterDict[monsterPrefab.name] = monsterPrefab;
    //    }
    //}

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        monsterList = new List<Monster>();
    }

    public IEnumerator StartWave(int waveNumber)
    {
        while (_monsterCount != _waveMonsterCount[waveNumber])
        {
            // 일정 시간 기다리고 
            yield return new WaitForSeconds(_spawnTime);

            // 몬스터 스폰
            SpawnMonster(waveNumber);

            // 몬스터 수 증가
            _monsterCount++;
        }
    }

    private void SpawnMonster(int waveNumber)
    {
        switch (waveNumber)
        {
            case 0:
                GameObject monsterPrefab = _monsterPrefabList[waveNumber]; // 프리팹 가져오기

                // 스폰지점 설정 -> 이미 됨.

                GameObject spawnMonster = Instantiate(monsterPrefab, _spawnPoint.position, Quaternion.identity); // 스폰 지점에 몬스터 생성 
                Monster monster = spawnMonster.GetComponent<Monster>();
                monsterList.Add(monster);
                monster.Init(waveNumber, _targetPoints, _gameManager.player.transform); // 몬스터 상태 초기화 -> 스탯, 이동, 플레이어 정보  

                break;
        }
    }
}