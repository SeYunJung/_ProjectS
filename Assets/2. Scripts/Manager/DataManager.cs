using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    #region Singleton

    private static DataManager _instance;
    public static DataManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    #endregion

    private ResourceManager _resourceManager;
    public Player player {  get; private set; }

    public void Init()
    {
        _resourceManager = ResourceManager.instance;

        player = _resourceManager.CreateCharacter<Player>(Prefab.PLAYER, Position.PLAYER);
    }
}
