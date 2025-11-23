using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorkerSummonButton : MonoBehaviour
{
    public void OnButtonClicked()
    {
        // 플레이어 돈이 충분하면
        if(GameManager.instance.player.ReturnGold() >= 10.0f)
        {
            // 골드 지불
            GameManager.instance.player.stat.SetMoney(-10.0f);

            // 일꾼 소환 
            GameManager.instance.workerSpawnManager.SpawnWorker();
        }
        else
        {
            StartCoroutine(GameManager.instance.uiManager.ActiveUINotEnoughMineral());
        }
    }
}
