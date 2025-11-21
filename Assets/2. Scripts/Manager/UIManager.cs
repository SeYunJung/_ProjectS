using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /*
    기능
    - UI를 키고 끄기.


    필요한 변수 
    - 승리 패배 UI
     */
    // 승리 UI
    // 패배 UI 
    // 얘네들 모두 띄워주는 용도로 쓰인다. 
    // 따라서 GameObject로 받아오자. 
    [SerializeField] private UIResult _uiResult;

    public void Active(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void InActive(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void ActiveResultPanel(int lastWaveNumber)
    {
        // 승리 UI 띄우기 
        //_uiResult.ShowResultPanel(finalWave);
        _uiResult.SetWaveText(lastWaveNumber);
        Active(_uiResult.gameObject);
    }

    public void GameOver()
    {
        // 패배 UI 띄우기 
        //Active(_uiResult);
    }
}
