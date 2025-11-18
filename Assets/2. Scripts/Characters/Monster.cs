using UnityEngine;
using UnityEngine.UIElements;

public class Monster : Character
{
    // 필요한 변수
    private Vector2 _movementDirection; // 움직이는 방향
    private Transform[] _targetPoints; // 목표지점들 
    private int _targetPointIndex;

    // 거리, 방향
    private float _distanceToTarget;
    private Vector3 _directionToTarget;
    private Vector3 _targetPos;

    // 몬스터 상태 초기화 -> 스탯, 이동, 플레이어 정보  
    public void Init(int waveNumber, Transform[] targetPoints, Transform player)
    {
        base.Init();

        _movementDirection = Vector2.zero;
        _targetPoints = targetPoints;

        switch (waveNumber)
        {
            case 0:
                stat.Init(waveNumber, moveSpeed, player); // 스탯 초기화 
                _targetPos = _targetPoints[_targetPointIndex].position;
                break;
        }
    }

    // 이동 처리
    private void Update()
    {
        Move();

        // 타겟 지점까지 도착했으면 다음 목표지점 지정후 이동 
        if(_distanceToTarget <= 0.01f)
        {
            if(_targetPointIndex < _targetPoints.Length)
            {
                _targetPos = _targetPoints[_targetPointIndex++].position;
            }
        }
    }

    private void Move()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetPos); // 거리
        _directionToTarget = (_targetPos - transform.position).normalized; // 방향 

        transform.position += (_directionToTarget * stat.speed) * Time.deltaTime;
    }
}