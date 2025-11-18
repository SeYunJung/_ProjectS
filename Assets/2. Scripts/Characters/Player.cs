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
    private float _attackRange;
    private float _distanceToTarget;
    private float _distanceToAttackTarget;
    private Transform _attackTargetTransform;
    private float _attackDelay;
    private State _currentState;
    private bool _isAttacking;

    // 참조 변수
    private GameManager _gameManager;
    private MonsterSpawnManager _monsterSpawnManager;
    private List<Monster> _monsterList;


    // 플레이어 초기화
    public override void Init()
    {
        // 참조 초기화
        base.Init();
        _gameManager = GameManager.instance;
        _monsterSpawnManager = _gameManager.monsterSpawnManager;
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
            // 플레이어와 _monsterList[i]번째 몬스터와의 거리 
            _distanceToTarget = Vector3.Distance(_monsterList[i].transform.position, transform.position);

            // 공격범위 안에 있고, 현재까지 검사한 적보다 거리가 가까우면 
            if (_distanceToTarget <= _attackRange && _distanceToTarget <= _closestDistSqr)
            {
                // 가장 가까운 거리 업데이트, 공격 대상 위치 저장 
                _closestDistSqr = _distanceToTarget;
                _attackTargetTransform = _monsterList[i].transform;
            }
        }

        // 공격 대상이 지정되면
        if(_attackTargetTransform != null)
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
        if (_attackTargetTransform == null)
        {
            // 탐색 상태로 전환
            _currentState = State.Search;
            yield return null;
        }

        // 타겟 몬스터가 공격 범위 안에서 벗어났으면
        _distanceToAttackTarget = Vector3.Distance(_attackTargetTransform.position, transform.position);
        if (_distanceToAttackTarget > _attackRange)
        {
            _attackTargetTransform = null;
            _currentState = State.Search;
            yield return null;
        }

        // 공격 쿨타임
        yield return new WaitForSeconds(_attackDelay);

        // 투사체 생성 
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        Debug.Log("투사체 생성");
    }
}
