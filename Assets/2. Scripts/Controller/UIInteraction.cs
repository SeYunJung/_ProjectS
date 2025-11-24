using System.Collections.Generic;
using System.Linq;
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
    private float level = 1;

    private GameObject _victimHero;

    // 리펙토링 필요. 씬이 전환되면 missing 에러 날 것 같음.
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tilemap _obstacleTilemap;
    [SerializeField] private Tile _normalTile;

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
                        // 돈이 충분하면
                        if (_player.ReturnGold() >= 20.0f)
                        {
                            // 재화 소모 
                            _player.GetComponent<CombatController>().stat.SetMoney(-20);

                            // 승급 UI 닫기
                            _uiManager.InActiveUIPromotion();

                            // 영웅 랜덤 생성 
                            Vector3 position = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));

                            _gameManager.heroSpawnManager.Promotion(_currentHero.GetComponent<Hero>(), position);
                            _gameManager.heroSpawnManager.Remove(_currentHero);
                            _gameManager.heroSpawnManager.Remove(_victimHero);
                        }
                        // 돈이 충분하지 않으면
                        else
                        {
                            // 승급 UI 닫기.
                            _uiManager.InActiveUIPromotion();

                            // 1.5초 동안 not enough money UI 띄우기. 
                            StartCoroutine(_uiManager.ActiveUINotEnoughMoney());
                        }
                        return;
                    }

                    // 안 쓰는 코드
                    // 초월 UI 클릭했으면
                    if (result.gameObject.layer == 12)
                    {
                        // 미네랄이 충분하면
                        if(_player.ReturnMineral() >= 1.0f)
                        {
                            // 재화 소모
                            _player.stat.SetMineral(-1.0f);

                            // 초월 UI 닫기
                            _uiManager.InActiveUIAwake();

                            // 영웅 초월 
                            Vector3 position = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));
                            _gameManager.heroSpawnManager.HeroAwake(_currentHero.GetComponent<Hero>(), position);
                            //_gameManager.heroSpawnManager.Promotion(_currentHero.GetComponent<Hero>(), position);
                            _gameManager.heroSpawnManager.Remove(_currentHero);
                        }
                        // 미네랄 충분하지 않으면
                        else
                        {
                            // 미네랄 부족 UI 띄우기. 1.5초간. 

                        }
                    }
                }
            }

            // 영웅을 클릭했으면
            if (_hit.collider != null && _hit.collider.gameObject.layer == 9)
            {
                _currentHero = _hit.collider.gameObject;

                // 영웅 레벨이 4이면 
                if(_currentHero.GetComponent<Hero>().level == 4)
                {
                    Debug.Log("초월하자.");

                    Hero currentHero = _currentHero.GetComponent<Hero>();
                    _victimHero = _gameManager.heroSpawnManager.heroList
                        .Where(x => x.level == currentHero.level && x != currentHero && x.name == _currentHero.name)
                        .Select(x => x.gameObject)
                        .FirstOrDefault();

                    // 초월 UI 띄우기.
                    _uiManager.ActiveUIAwake(_currentHitPos);

                    // (다른 곳에서) 초월 UI를 클릭하면 
                        // 미네랄이 충분하다면
                            // 영웅 초월 
                    return;
                }

                // 영웅 레벨이 4이상이면 종료 
                if (!(_currentHero.GetComponent<Hero>().level < 4)) return;


                // 클릭한 영웅과 같은 영웅이 블록에 배치되어 있다면 (돈 충분한지 여부 확인 필요 x)
                if(_gameManager.heroSpawnManager.heroList.Count(x => x.level == _currentHero.GetComponent<Hero>().level && x.name == _currentHero.name) >= 2)
                {
                    Hero currentHero = _currentHero.GetComponent<Hero>();
                    _victimHero = _gameManager.heroSpawnManager.heroList
                        .Where(x => x.level == currentHero.level && x != currentHero && x.name == _currentHero.name)
                        .Select(x => x.gameObject)
                        .FirstOrDefault();

                    // 승급 UI 열기.
                    _uiManager.ActiveUIPromotion(_currentHitPos);

                    return;
                }
            }

            //if (_currentHero != null && _currentHero.GetComponent<Hero>().level < 4) return;

            // 일반 블록, 스피드 블록을 클릭하면 
            if (_hit.collider != null && (_hit.collider.gameObject.layer == 8 || _hit.collider.gameObject.layer == 11))
            {
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
                return;
            }

            // 장애물 블록을 클릭하면
            if (_hit.collider != null && _hit.collider.gameObject.layer == 13)
            {
                _currentHitPos = _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_mouseWorldPos));

                // 수리 UI 열기.
                _uiManager.ActiveUIRepair(_currentHitPos);

                return;
            }
        }
        
    }

    public Vector3 GetCurrentHitPos()
    {
        return _currentHitPos;
    }

    public GameObject GetTargetInfo()
    {
        return _currentHero;
    }

    public GameObject GetVictimHeroInfo()
    {
        return _victimHero;
    }

    public void RemoveTile()
    {
        Vector3Int removeTilePos = _obstacleTilemap.WorldToCell(_mouseWorldPos);
        _obstacleTilemap.SetTile(removeTilePos, null);
    }

    public void SetTile()
    {
        Vector3Int setTilePos = _obstacleTilemap.WorldToCell(_mouseWorldPos);
        _tilemap.SetTile(setTilePos, _normalTile);
    }
}
