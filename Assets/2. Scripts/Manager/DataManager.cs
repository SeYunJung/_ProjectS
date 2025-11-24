using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private ResourceManager _resourceManager;
    public Player player {  get; private set; }

    public void Init()
    {
        _resourceManager = ResourceManager.instance;

        player = _resourceManager.CreateCharacter<Player>(Prefab.PLAYER, Position.PLAYER);
    }
}
