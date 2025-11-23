using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker : MonoBehaviour
{
    /*
     
    일꾼 

    역할
    - 센터에서 리스폰 (처음 리스폰되는 일꾼은 오른쪽 광물로 향함)
    - 일꾼이 광물과 닿으면. 광물 이미지 활성화, 이동방향 반대로 전환하고 센터로 이동
    - 광물 이미지가 활성화된 상태에서 센터와 닿으면. 플레이어 광물 늘리기, 광물 이미지 비활성화, 이동방향 반대로 전환하고 광물로 이동

     */

    // 이동 변수
    private Vector3 _moveDirection;
    private float _moveSpeed;
    [SerializeField] private List<Vector3> _workingPosList;
    private Transform[] targetPoints;
    private Vector3 _targetPos;
    private float _distanceToTarget;
    private float _distanceToCenter;

    // 이미지
    [SerializeField] private GameObject _mineralImage;

    public void Init(bool flag, Transform[] targetPoints)
    {
        _moveSpeed = 1.0f;
        this.targetPoints = targetPoints;

        if (flag)
        {
            // 오른쪽
            _moveDirection = Vector3.right;
            //_targetPos = _workingPosList[1];
            _targetPos = this.targetPoints[1].position;
        }
        else
        {
            // 왼쪽 
            _moveDirection = Vector3.left;
            //_targetPos = _workingPosList[0];
            _targetPos = this.targetPoints[0].position;
        }
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetPos);
        //_distanceToCenter = Vector3.Distance(transform.position, _workingPosList[2]);
        _distanceToCenter = Vector3.Distance(transform.position, this.targetPoints[2].position);

        transform.position += (_moveDirection * _moveSpeed) * Time.deltaTime;

        if(_distanceToTarget <= 0.01f && !_mineralImage.activeSelf)
        {
            // 광물 이미지 활성화
            _mineralImage.SetActive(true);

            // 이동방향 반대로 전환
            Vector3 pos = _moveDirection;
            pos.x = -pos.x;
            _moveDirection = pos;
        }

        // 광물 이미지가 활성화된 상태에서 센터와 닿으면
        if(_mineralImage.activeSelf && _distanceToCenter <= 0.1f)
        {
            // 플레이어 광물 늘리기
            if(_moveDirection == Vector3.left)
            {
                GameManager.instance.player.stat.SetMineral(1.0f);
            }
            else if(_moveDirection == Vector3.right)
            {
                GameManager.instance.player.stat.SetMineral(2.0f);
            }

            // 광물 이미지 비활성화
            _mineralImage.SetActive(false);

            // 이동방향 반대로 전환하고 광물로 이동
            Vector3 pos = _moveDirection;
            pos.x = -pos.x;
            _moveDirection = pos;
        }
    }
}
