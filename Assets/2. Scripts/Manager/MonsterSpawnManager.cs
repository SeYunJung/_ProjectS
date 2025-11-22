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
    [SerializeField] private float _waveDelay;
    [SerializeField] private float _clearDelay;
    private int _monsterCount;
    public List<Monster> monsterList { get; private set; }

    private GameManager _gameManager;

    // 스폰할 몬스터들 저장 -> 필요없다. => 특정 몬스터(보스)를 지정해서 스폰할 필요가 없기 때문 
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

    public IEnumerator WaveStart()
    {
        for(int i = 0; i < _waveMonsterCount.Length; i++)
        {
            Debug.Log($"{i+1}번 웨이브 시작");

            // i번째 웨이브 시작
            while(_monsterCount != _waveMonsterCount[i])
            {
                yield return new WaitForSeconds(_spawnTime);

                // i번 웨이브 몬스터 스폰
                SpawnMonster(i);

                // i번 웨이브 몬스터 수 증가
                _monsterCount++;
            }

            Debug.Log($"{i+1}번 웨이브 몬스터 소환 끝");
            _monsterCount = 0;

            _gameManager.UpdateCurrentWaveNumber(i + 1);
            
            // n초 후 다음 웨이브 시작. 
            yield return new WaitForSeconds(_waveDelay);
            Debug.Log("다음 웨이브가 시작됩니다.");
        }
    }

    // 리펙토링 필요
    // - n번 웨이브에 여러 종류 몬스터가 나올 수 있게 대비 (여러 몬스터 == 몬스터1, 몬스터2, 보스1, 보스2
    // - 중복 코드 하나로 통합 
    private void SpawnMonster(int waveNumber)
    {
        switch (waveNumber)
        {
            case 0:

                GameObject spawnMonster = Instantiate(_monsterPrefabList[waveNumber], _spawnPoint.position, Quaternion.identity); // 스폰 지점에 몬스터 생성 
                Monster monster = spawnMonster.GetComponent<Monster>();
                monsterList.Add(monster);
                monster.Init(waveNumber, _targetPoints, _gameManager.player.transform); // 몬스터 상태 초기화 -> 스탯, 이동, 플레이어 정보  

                break;
            case 1:

                GameObject spawnMonster1 = Instantiate(_monsterPrefabList[waveNumber], _spawnPoint.position, Quaternion.identity);
                Monster monster1 = spawnMonster1.GetComponent<Monster>();
                monsterList.Add(monster1);
                monster1.Init(waveNumber, _targetPoints, _gameManager.player.transform);

                break;
        }
    }
}