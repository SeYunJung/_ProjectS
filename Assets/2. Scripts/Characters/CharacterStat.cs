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
    public float money {  get; private set; }
    public float mineral {  get; private set; }
    public float workerCount { get; private set; }

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
        this.money = 100000.0f;
        this.mineral = 0.0f;
        this.workerCount = 1.0f;
    }

    public void SetHealth(float value)
    {
        this.health += value;
    }

    public void SetMoney(float money)
    {
        this.money += money;

        // UI 업데이트
        GameManager.instance.uiManager.uiResourcePanel.UpdateMoney(this.money);
    }

    public void SetMineral(float mineral)
    {
        this.mineral += mineral;

        GameManager.instance.uiManager.uiResourcePanel.UpdateMineral(this.mineral);
    }
}
