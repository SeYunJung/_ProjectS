using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    // 공통 스탯
    public float health { get; private set; }
    public Transform target { get; private set; }

    // 플레이어 스탯
    public float attackSpeed { get; private set; }
    public float gold {  get; private set; }

    // 몬스터 스탯
    public float speed { get; private set; }


    // 몬스터 스탯 초기화 
    public void Init(int waveNumber, float speed, Transform target)
    {
        switch (waveNumber)
        {
            case 0:
                this.health = 5.0f;
                this.speed = speed;
                this.target = target;
                break;
            case 1:
                this.health = 1.0f;
                this.speed = speed;
                this.target = target;
                break;
        }
    }

    // 플레이어 스탯 초기화
    public void Init()
    {
        this.health = 10.0f;
        this.attackSpeed = 1.0f;
        this.gold = 100.0f;
    }

    public void SetHealth(float value)
    {
        health += value;
    }
}
