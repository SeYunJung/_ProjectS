using System.Collections;
using UnityEngine;

public enum UIState
{
    Close,
    Open,
}

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
    [SerializeField] private RectTransform _uiMonsterSummonRect;
    [SerializeField] private UISummonButton _uiSummonButton;
    [SerializeField] private GameObject _uiNotEnoughMoney;
    [SerializeField] private RectTransform _uiPromotionRect;

    private Vector3 _blockOffset;

    public void Init()
    {
        _uiSummonButton.Init();

        _blockOffset = new Vector3(Pos.BLOCK_OFFSET_X, Pos.BLOCK_OFFSET_Y);
    }

    // 가능하다면 활성화/비활성화 메서드 통합할 수 있게 리펙토링
    public void SetActive(GameObject ui, bool isActive)
    {
        ui.SetActive(isActive);
    }

    public void ActiveUIResult(int lastWaveNumber)
    {
        _uiResult.SetWaveText(lastWaveNumber);
        SetActive(_uiResult.gameObject, true);
    }

    public void ActiveUIMonsterSummon(Vector3 worldPos)
    {
        _uiMonsterSummonRect.position = worldPos + _blockOffset;
        SetActive(_uiMonsterSummonRect.gameObject, true);
    }

    public void InActiveUIMonsterSummon()
    {
        SetActive(_uiMonsterSummonRect.gameObject, false);
    }

    public bool IsActiveUIMonsterSummon()
    {
        return _uiMonsterSummonRect.gameObject.activeSelf;
    }

    public IEnumerator ActiveUINotEnoughMoney()
    {
        SetActive(_uiNotEnoughMoney, true);

        yield return new WaitForSeconds(1.5f);

        InActiveUINotEnoughMoney();
    }

    public void InActiveUINotEnoughMoney()
    {
        SetActive(_uiNotEnoughMoney, false);
    }

    public void ActiveUIPromotion(Vector3 worldPos)
    {
        //_uiMonsterSummonRect.position = worldPos + _blockOffset;
        //SetActive(_uiMonsterSummonRect.gameObject, true);

        _uiPromotionRect.position = worldPos + _blockOffset;
        SetActive(_uiPromotionRect.gameObject, true);
    }

    public void InActiveUIPromotion()
    {
        SetActive(_uiPromotionRect.gameObject, false);
    }
}
