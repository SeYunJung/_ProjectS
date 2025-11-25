using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager : MonoBehaviour_Singleton<ObjectManager>
{
    // 부모 오브젝트
    private GameObject _field;
    public GameObject spawnPoint {  get; private set; }

    private ResourceManager _resourceManager;

    // 외부 참조 
    public Player player {  get; private set; }
    public Grid grid { get; private set; }
    public GameObject uiPromotion {  get; private set; }

    public void Init()
    {
        _resourceManager = ResourceManager.instance;

        // 부모 오브젝트
        _field = _resourceManager.CreateParentObject<GameObject>(Prefab.FIELD, Vector2.zero);
        spawnPoint = _resourceManager.CreateParentObject<GameObject>(Prefab.MONSTER_SPAWNPOINT, Position.MONSTER);

        player = _resourceManager.CreateCharacter<Player>(Prefab.PLAYER, Position.PLAYER, _field.transform);
        grid = _resourceManager.CreateTilemap<Grid>(Prefab.GRID, Vector2.zero, _field.transform);
        uiPromotion = _resourceManager.CreateUI<GameObject>(Prefab.PROMOTION, Vector2.zero);
    }
}
