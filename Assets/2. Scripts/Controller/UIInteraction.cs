using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

// 리펙토링 : 이름 수정 필요 => UI 스크립트인지 상호작용 스크립트인지 헷갈림. 
public class UIInteraction : MonoBehaviour
{
    private Vector2 _mouseWorldPos;
    private RaycastHit2D _hit;
    private Vector3 _lastHitPos;
    private Vector3 _currentHitPos;

    public static UIState _uiState;

    private GameObject _currentHero;
    public int currentTileLayer {  get; private set; }

    // 리펙토링 필요. 씬이 전환되면 missing 에러 날 것 같음.
    [SerializeField] private Tilemap _tilemap;

    // 참조 변수
    private UIManager _uiManager;
    private GameManager _gameManager;
    private Player _player;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;

    public void Init()
    {
        _gameManager = GameManager.instance;
        _uiManager = _gameManager.uiManager;
        _player = _gameManager.player;
        _uiState = UIState.Close;
    }

    public void OnUIInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _hit = Physics2D.Raycast(_mouseWorldPos, Vector2.zero);

            if (_graphicRaycaster != null)
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();

                _graphicRaycaster.Raycast(pointerEventData, results);

                foreach(RaycastResult result in results)
                {
                    // 승급 UI 클릭했으면 
                    if(result.gameObject.layer == 10)
                    {
                        // 재화 소모 
                        _player.GetComponent<CombatController>().stat.SetMoney(-20);

                        // 승급 UI 닫기
                        _uiManager.InActiveUIPromotion();

                        // 영웅 랜덤 생성 
                        Vector3 position = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));
                        _gameManager.heroSpawnManager.Remove(_currentHero);
                        _gameManager.heroSpawnManager.Promotion(position);
                        break;
                    }
                }
            }

            // 영웅을 클릭했으면
            if (_hit.collider != null && _hit.collider.gameObject.layer == 9)
            {
                _currentHero = _hit.collider.gameObject;
                // 승급할 수 있는가? (일단은 돈이 충분한지 체크)
                if (_player.ReturnGold() >= 20.0f)
                {
                    // 승급 UI 띄우기.
                    _uiManager.ActiveUIPromotion(_currentHitPos);

                    return;
                }
                else
                {
                    Debug.Log("돈 부족");
                }
            }

            switch (_uiState)
            {
                case UIState.Close:
                    // 리펙토링 : 레이어를 번호로 구분하지 말고 2비트로 구분하는 건 어떤지?
                    if (_currentHitPos != _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos)))
                    {
                        _lastHitPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));
                        currentTileLayer = _hit.collider.gameObject.layer;

                        _uiManager.ActiveUIMonsterSummon(_lastHitPos);
                        _uiState = UIState.Open;
                    }
                    //if (_hit.collider != null && _hit.collider.gameObject.layer == 8)
                    //{
                    //    _lastHitPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));

                    //    _uiManager.ActiveUIMonsterSummon(_lastHitPos);
                    //    _uiState = UIState.Open;
                    //}
                    break;

                case UIState.Open:
                    // 몬스터 생성 UI가 켜져있으면, 다른 곳을 클릭했다면 
                    // 두 벡터 사이 거리가 별로 길지 않으면 -> 같은 블록을 클릭한 것으로 간주 -> UI 안사라지게 하기. 
                    // 그냥 현재 클릭한 타일 중심 좌표랑 이전 타일 중심 좌표랑 같은지만 체크하면 되겠는데.

                    if (_hit.collider != null)
                    {
                        _currentHitPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));
                    }

                    if (_hit.transform == null || _currentHitPos != _lastHitPos)
                    {
                        // 끄기 
                        _uiManager.InActiveUIMonsterSummon();
                        _uiState = UIState.Close;
                    }
                    break;
            }
        }
        
    }

    public Vector3 GetCurrentHitPos()
    {
        return _currentHitPos;
    }
}
