using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
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

    [SerializeField] private GameObject _projectilePrefab; // 투사체 
    [SerializeField] private float _projectileSpeed; // 투사체 이동 속도
    private Transform _monsterTransform; // 범위 내 있는 적 하나에 대한 정보

    [SerializeField] private float _attackRange; // 공격범위 
    private float _distanceToMonster; // 몬스터와 거리 

    private void Update()
    {
        /*
         
        공격 범위 안에 몬스터가 있다면
            그 몬스터를 타겟으로 설정
            그 몬스터를 공격 
         
         */
    }
}
