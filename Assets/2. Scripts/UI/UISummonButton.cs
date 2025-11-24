using UnityEngine;
using static Interaction;

public class UISummonButton : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private HeroSpawnManager _heroSpawnManager;
    private Player _player;

    public void Init()
    {
        _gameManager = GameManager.instance;
        _uiManager = _gameManager.uiManager;
        _heroSpawnManager = _gameManager.heroSpawnManager;
        _player = _gameManager.player;
    }

    public void OnClickedButton()
    {
        // 돈이 충분하면
        if (_player.ReturnGold() >= 20.0f)
        {
            // 재화 소모 
            Debug.Log($"수정 전 돈 : {_player.ReturnGold()}");
            _player.GetComponent<CombatController>().stat.SetMoney(-20);
            Debug.Log($"수정 후 돈 : {_player.ReturnGold()}");

            // 소환 UI 닫기.
            _uiManager.InActiveUIMonsterSummon();

            // 리펙토링 : 소환 UI 상태패턴으로 구현해야 함. 
            Interaction._uiState = UIState.Close;

            Vector3 position = _player.GetCurrentHitPos();

            // 녹색 타일이면 
            if (_player.interaction.currentTileLayer == 11)
            {
                // 속도 += 0.3f 
                _heroSpawnManager.SpawnHero(position, 10f);
            }
            else
            {
                _heroSpawnManager.SpawnHero(position);
            }
        }
        else
        {
            // 돈 부족 UI 열기
            StartCoroutine(_uiManager.ActiveUINotEnoughMoney());
        }
    }
}
