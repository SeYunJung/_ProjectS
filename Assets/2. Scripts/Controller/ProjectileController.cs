using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    /*
    
    공격에 필요한 정보
    - 투사체 
    - 투사체 이동 속도
    - 범위 내 있는 적 하나에 대한 정보 
    
    투사체의 역할
    - 적과 닿으면 사라진다.
    - 적 체력을 감소시킨다. 

     */

    //[SerializeField] private GameObject _projectilePrefab; // 투사체 
    ////[SerializeField] private float _projectileSpeed; // 투사체 이동 속도
    //private Transform _monsterTransform; // 범위 내 있는 적 하나에 대한 정보

    //[SerializeField] private float _attackRange; // 공격범위 
    //private float _distanceToMonster; // 몬스터와 거리 

    //private void Update()
    //{
    //    /*

    //    공격 범위 안에 몬스터가 있다면
    //        그 몬스터를 타겟으로 설정
    //        그 몬스터를 공격 

    //     */
    //}

    // 투사체 이동 스크립트

    /*
    역할
    - 투사체 이동
    - 투사체가 목표와 닿으면 목표 체력 낮추기, 투사체 사라지기 

    필요한 것
    - 투사체 이동 : 좌표 이동 
    스타디는 몬스터가 공격받을 때 밀려나던가? (물리처리가 있는가?) -> 특정조건(?)에서 경직되는 것 말고는 밀려나가거나 하는 처리가 없다. -> 투사체 이동도 좌표로 처리하자. 
    
    - 투사체가 닿았을 때 목표 체력 낮추기, 투사체 사라지기

    - 투사체 초기화 함수 : 목표 위치, 이동속도, 목표를 바라보는 방향 초기화 

    - 변수
    목표를 바라보는 방향
    투사체 이동속도
    목표 위치 

     */

    private Monster _target;
    private Transform _targetTransform;
    private Vector3 _directionToTarget;
    private float _distanceToTarget;
    private float _projectileSpeed;
    [SerializeField] private float _power;

    // 참조 변수
    private GameManager _gameManager;
    private List<Monster> _monsterList;


    // 투사체 초기화 함수
    // 목표 위치, 목표를 바라보는 방향 초기화 
    public void Init(Monster monster) 
    {
        _target = monster;
        _targetTransform = monster.transform;
        _projectileSpeed = 4.0f;

        _gameManager = GameManager.instance;
        _monsterList = GameManager.instance.monsterSpawnManager.monsterList;
    }

    // 투사체 이동
    private void Update()
    {
        if (_target != null)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _targetTransform.position); // 거리
            _directionToTarget = (_targetTransform.position - transform.position).normalized; // 방향

            transform.position += (_directionToTarget * _projectileSpeed) * Time.deltaTime;

            // 몬스터에 근접해지면 
            if (_distanceToTarget <= 0.01f)
            {
                gameObject.SetActive(false);

                // 몬스터 체력 감소
                _target.SetHealth(-_power);

                // 몬스터 체력이 0미만이면
                if(_target.GetHealth() < 0)
                {
                    // 몬스터 파괴 
                    Destroy(_target.gameObject);
                    _monsterList.Remove(_target);

                    // 마지막 웨이브이면서 monsterList 크기가 0이면 
                    if (_gameManager.isEndGame() && _monsterList.Count == 0)
                    {
                        // 게임 종료 
                        _gameManager.EndGame();
                    }
                }
            }
        }
    }
}
