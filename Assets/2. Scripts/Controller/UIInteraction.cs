using UnityEngine;
using UnityEngine.Tilemaps;

public class UIInteraction : MonoBehaviour
{
    private Vector2 _mouseWorldPos;
    private RaycastHit2D _hit;

    [SerializeField] private Tilemap _tilemap;

    // 참조 변수
    private UIManager _uiManager;

    public void Init()
    {
        _uiManager = GameManager.instance.uiManager;
    }

    public void OnUIInteraction()
    {
        _uiManager = GameManager.instance.uiManager;

        _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _hit = Physics2D.Raycast(_mouseWorldPos, Vector2.zero);

        if (_hit.collider != null && _hit.collider.gameObject.layer == 8)
        {
            Vector3 worldPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));

            _uiManager.ActiveUIMonsterSummon(worldPos);
        }
        else
        {
            // 몬스터 생성 UI가 켜져있으면
            if (_uiManager.IsActiveUIMonsterSummon())
            {
                // 끄기 
                _uiManager.InActiveUIMonsterSummon();
            }
        }
    }
}
