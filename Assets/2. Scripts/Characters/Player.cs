using UnityEngine;

public class Player : CombatController
{
    public float ReturnGold()
    {
        return stat.money;
    }

    public float ReturnMineral()
    {
        return stat.mineral;
    }

    public void UpdateGold(float money)
    {
        stat.SetMoney(money);
    }

    public Vector3 GetCurrentHitPos()
    {
        return interaction.GetCurrentHitPos();
    }

    public GameObject GetTargetInfo()
    {
        return interaction.GetTargetInfo();
    }

    public GameObject GetVictimHeroInfo()
    {
        return interaction.GetVictimHeroInfo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 리펙토링 : 레이어 번호 -> 이진수로 
        if(collision.gameObject.layer == 7)
        {
            // 플레이어 체력 감소
            stat.SetHealth(-5);

            // 플레이어 체력 < 0
            if(stat.health < 0)
            {
                // 게임오버 
                gameManager.EndGame();
            }
        }
    }
}
