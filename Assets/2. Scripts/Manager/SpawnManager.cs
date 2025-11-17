using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
     
    적을 생성 

    적 생성에 필요한 정보
    - 스폰 지점
    - 적 오브젝트 (프리팹)
    
    적을 생성하는 방법
    - Instantiate

     */

    [SerializeField] private Transform _spawnPointTransform;
    [SerializeField] private GameObject _monsterPrefab;

    private void Awake()
    {
        Instantiate(_monsterPrefab, _spawnPointTransform);
    }
}
