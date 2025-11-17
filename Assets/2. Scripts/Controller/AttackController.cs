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

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    private Transform _monsterTransform;
    [SerializeField] private float _attackRange;
    private float _distanceToMonster;

    private void Update()
    {
        
    }
}
