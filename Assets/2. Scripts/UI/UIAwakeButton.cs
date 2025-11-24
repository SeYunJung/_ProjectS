using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAwakeButton : MonoBehaviour
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
        // 미네랄이 충분하면
        if (_player.ReturnMineral() >= 10.0f)
        {
            _player.GetComponent<CombatController>().stat.SetMineral(-10);

            // 초월 UI 닫기.
            _uiManager.InActiveUIAwake();

            UIInteraction._uiState = UIState.Close;

            Vector3 position = _player.GetCurrentHitPos();
            GameObject target = _player.GetTargetInfo();
            GameObject victimHero = _player.GetVictimHeroInfo();

            _gameManager.heroSpawnManager.Remove(target);
            _gameManager.heroSpawnManager.Remove(victimHero);
            _heroSpawnManager.SpawnAwakeHero(position);
        }
        else
        {
            StartCoroutine(_uiManager.ActiveUINotEnoughMineral());
        }
    }
}
