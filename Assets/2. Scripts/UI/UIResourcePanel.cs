using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourcePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _uiMoneyText;
    [SerializeField] private TMP_Text _uiMineralText;
    [SerializeField] private TMP_Text _uiWorkerText;
    [SerializeField] private Button _uiMissionButton;
    [SerializeField] private Button _uiResourceButton;
    [SerializeField] private Button _uiUpgradeButton;

    public void Init()
    {
        _uiMoneyText.text = "100000";
        _uiMineralText.text = "0";
        _uiWorkerText.text = "1";
    }

    // 플레이어가 자원(돈, 미네랄)을 획득할 때마다 호출
    public void UpdateMoney(float money)
    {
        _uiMoneyText.text = money.ToString();
    }
    public void UpdateMineral(float mineral)
    {
        _uiMineralText.text = mineral.ToString();
    }

    public void UpdateWorkerCount(int workerCount)
    {
        _uiWorkerText.text = workerCount.ToString();
    }
}
