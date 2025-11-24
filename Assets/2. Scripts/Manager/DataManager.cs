using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private ResourceManager _resourceManager;
    public Player player {  get; private set; }
    public Grid grid { get; private set; }
    public GameObject uiPromotion {  get; private set; }

    public void Init()
    {
        _resourceManager = ResourceManager.instance;

        player = _resourceManager.CreateCharacter<Player>(Prefab.PLAYER, Position.PLAYER);
        grid = _resourceManager.CreateTilemap<Grid>(Prefab.GRID, Vector2.zero);
        uiPromotion = _resourceManager.CreateUI<GameObject>(Prefab.PROMOTION, Vector2.zero);
    }
}
