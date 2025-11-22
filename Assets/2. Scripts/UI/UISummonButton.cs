using UnityEngine;
using static UIInteraction;

public class UISummonButton : MonoBehaviour
{
    // 플레이어 돈 정보 필요함 

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
            Debug.Log("돈 충분!");

            // 소환 UI 닫기.
            _uiManager.InActiveUIMonsterSummon();
            // _uiState = UIState.Close; // 상태 전환 

            // 리펙토링 : 소환 UI 상태패턴으로 구현해야 함. 
            UIInteraction._uiState = UIState.Close;

            Vector3 position = _player.GetCurrentHitPos();
            _heroSpawnManager.SpawnHero(position);
            // 랜덤 영웅 생성 (스폰 담당)
            // 생성된 영웅은 초기화를 거쳐 자동공격한다. (스폰 담당)
        }
    }
}
