using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    /*
     
    적을 이동시키는 방법
    - 매 프레임마다 좌표이동

    적 이동에 필요한 정보
    - 이동할 지점
    - 이동 속도
     
     */

    private Vector3 _directionToTarget; // 목표를 바라보는 방향 
    [SerializeField] private Vector3 _targetPosition; // 목표 지점 
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        //_directionToTarget = (_targetPosition - transform.position).normalized;
        //transform.position += (_directionToTarget * _moveSpeed) * Time.deltaTime;
    }
}
