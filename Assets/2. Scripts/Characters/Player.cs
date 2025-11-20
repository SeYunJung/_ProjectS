using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Search,
    Attack
}

public class Player : Character
{
    // 플레이어 고유 변수
    private float _closestDistSqr;
    [SerializeField] private float _attackRange;
    private float _distanceToTarget;
    private float _distanceToAttackTarget;
    private Monster _targetMonster;
    [SerializeField] private float _attackDelay;
    private State _currentState;
    private bool _isAttacking;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawnPoint;

    // 참조 변수
    private GameManager _gameManager;
    private MonsterSpawnManager _monsterSpawnManager;
    private ObjectPool _objectPool;
    private List<Monster> _monsterList;


    // 플레이어 초기화
    public override void Init()
    {
        // 참조 초기화
        base.Init();
        _gameManager = GameManager.instance;
        _monsterSpawnManager = _gameManager.monsterSpawnManager;
        _objectPool = _gameManager.objectPool;
        _monsterList = _monsterSpawnManager.monsterList;

        // 스탯 초기화 
        stat.Init(); 

        // 상태 초기화
        _currentState = State.Search;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Search:
                SearchTarget();
                break;
            case State.Attack:
                _isAttacking = true;
                break;
        }
    }

    private void SearchTarget()
    {
        _closestDistSqr = Mathf.Infinity;

        for (int i = 0; i < _monsterList.Count; i++)
        {
            if (_monsterList[i] == null) continue;
            // 플레이어와 _monsterList[i]번째 몬스터와의 거리 
            _distanceToTarget = Vector3.Distance(_monsterList[i].transform.position, transform.position);

            // 공격범위 안에 있고, 현재까지 검사한 적보다 거리가 가까우면 
            if (_distanceToTarget <= _attackRange && _distanceToTarget <= _closestDistSqr)
            {
                // 가장 가까운 거리 업데이트, 공격 대상 위치 저장 
                _closestDistSqr = _distanceToTarget;
                _targetMonster = _monsterList[i];
            }
        }

        // 공격 대상이 지정되면
        if(_targetMonster != null)
        {
            // 공격 상태로 전환
            _currentState = State.Attack;
        }

        // 몬스터가 공격범위 안에 있으면 
            // 투사체 생성
            // 투사체가 적에게 이동
            // 투사체가 적에게 닿으면 사라지기
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        // 타겟 몬스터가 없으면
        if (_targetMonster == null)
        {
            // 탐색 상태로 전환
            _currentState = State.Search;
            //yield return null;
            yield break;
        }

        // 타겟 몬스터가 공격 범위 안에서 벗어났으면
        _distanceToAttackTarget = Vector3.Distance(_targetMonster.transform.position, transform.position);
        if (_distanceToAttackTarget > _attackRange)
        {
            _targetMonster = null;
            _currentState = State.Search;
            //yield return null;
            yield break;
        }

        // 투사체 생성 
        SpawnProjectile();

        // 공격 쿨타임
        Debug.Log("쿨타임 시작");
        yield return new WaitForSeconds(_attackDelay);
        Debug.Log("쿨타임 끝");
    }

    private void SpawnProjectile()
    {
        //
        GameObject obj = _objectPool.SpawnFromPool("jsy", _projectileSpawnPoint.position, Quaternion.identity);

        bool isProjectile = obj.TryGetComponent(out ProjectileController projectileController);
        if (isProjectile)
        {
            projectileController.Init(_targetMonster); // 목표 위치, 목표를 바라보는 방향 초기화 
        }

        //GameObject go = Instantiate(_projectile, _projectileSpawnPoint);

        //bool isProjectile = go.TryGetComponent(out ProjectileController projectileController);
        //if (isProjectile)
        //{
        //    // 초기화 
        //    // 어떤 초기화를 하는데? 목표대상 정보를 주면 그 대상으로 투사체가 날라갈 수 있도록 하는 정보를 초기화한다. 
        //    // 그러면 목표대상 정보만 주면 되겠네. 
        //    projectileController.Init(_targetMonster); // 목표 위치, 목표를 바라보는 방향 초기화 
        //}

        //// 일단은 그냥 생성하자. 
        //// 그리고나서 뭐가 안 좋은게 있으면 오브젝트 풀링 적용하자. 
    }
}
