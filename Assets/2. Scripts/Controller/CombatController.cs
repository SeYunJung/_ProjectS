using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : Character
{
    public enum State
    {
        Search,
        Attack
    }

    // 플레이어 고유 변수
    private float _closestDistSqr;
    [SerializeField] private float _attackRange;
    private float _distanceToTarget;
    private float _distanceToAttackTarget;
    public Monster _targetMonster; // private
    [SerializeField] private float _attackDelay;
    public State _currentState; // private
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawnPoint;

    // 참조 변수
    protected GameManager gameManager;
    private MonsterSpawnManager _monsterSpawnManager;
    private ObjectPool _objectPool;
    private List<Monster> _monsterList;
    public Interaction interaction {  get; private set; }

    // 플레이어 초기화
    public override void Init()
    {
        // 참조 초기화
        base.Init();
        gameManager = GameManager.instance;
        _monsterSpawnManager = gameManager.monsterSpawnManager;
        _objectPool = gameManager.objectPool;
        _monsterList = _monsterSpawnManager.monsterList;
        interaction = GetComponent<Interaction>();
        interaction.Init();

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
        if (_targetMonster != null)
        {
            // 공격 상태로 전환
            _currentState = State.Attack;
            StartCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        while (_currentState == State.Attack)
        {
            // 타겟 몬스터가 없으면
            if (_targetMonster == null)
            {
                // 탐색 상태로 전환
                _currentState = State.Search;
                yield break;
            }

            // 타겟 몬스터가 공격 범위 안에서 벗어났으면
            _distanceToAttackTarget = Vector3.Distance(_targetMonster.transform.position, transform.position);
            if (_distanceToAttackTarget > _attackRange)
            {
                _targetMonster = null;
                _currentState = State.Search;
                yield break;
            }

            // 투사체 생성 (X) -> 총알 발사 (O) 
            ShootProjectile();

            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void ShootProjectile()
    {
        GameObject obj = _objectPool.SpawnFromPool("jsy", _projectileSpawnPoint.position, Quaternion.identity);

        bool isProjectile = obj.TryGetComponent(out ProjectileController projectileController);
        if (isProjectile)
        {
            projectileController.Init(_targetMonster, attackSpeed); // 목표 위치, 목표를 바라보는 방향 초기화 
        }
    }
}
