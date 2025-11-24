using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRepairButton : MonoBehaviour
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

    // 수리 UI를 클릭하면
    public void OnClickedButton()
    {
        // 미네랄이 충분하면
        if (_player.ReturnMineral() >= 10.0f)
        {
            _player.GetComponent<CombatController>().stat.SetMineral(-10);

            // 수리 UI 닫기.
            _uiManager.InActiveUIRepair();

            // 해당 블록을 삭제
            _player.interaction.RemoveTile();

            // 일반 블록 넣기. 
            _player.interaction.SetTile();

                    

            //UIInteraction._uiState = UIState.Close;

            //Vector3 position = _player.GetCurrentHitPos();
            //GameObject target = _player.GetTargetInfo();
            //GameObject victimHero = _player.GetVictimHeroInfo();

            //_gameManager.heroSpawnManager.Remove(target);
            //_gameManager.heroSpawnManager.Remove(victimHero);
            //_heroSpawnManager.SpawnAwakeHero(position);
        }
        // 미네랄이 충분하지 않으면
        else
        {
            // 1.5초 동안 미네랄이 충분하지 않음 UI 띄우기.
            StartCoroutine(_uiManager.ActiveUINotEnoughMineral());

            // 수리 UI 닫기.
            _uiManager.InActiveUIRepair();
        }
    }
}
