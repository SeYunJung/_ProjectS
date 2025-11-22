using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : MonoBehaviour
{
    public enum HeroType
    {
        Warrior,
        Archer,
        Mage,
        Count
    }

    [SerializeField] private List<GameObject> _heroPrefabList; // 영웅 프리팹 리스트
    public List<Monster> heroList { get; private set; }

    public void Init()
    {
        heroList = new List<Monster>();
    }

    // 리펙토링 : 중복코드 제거 
    public void SpawnHero(Vector3 position)
    {
        int randomNumber = Random.Range(0, (int)HeroType.Count);
        HeroType heroType = (HeroType)randomNumber;

        switch (heroType)
        {
            case HeroType.Warrior:
                GameObject spawnHero = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero = spawnHero.GetComponent<Hero>();
                hero.Init();
                break;

            case HeroType.Archer:
                GameObject spawnHero1 = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero1 = spawnHero1.GetComponent<Hero>();
                hero1.Init();
                break;

            case HeroType.Mage:
                GameObject spawnHero2 = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero2 = spawnHero2.GetComponent<Hero>();
                hero2.Init();
                break;
        }
    }
}
