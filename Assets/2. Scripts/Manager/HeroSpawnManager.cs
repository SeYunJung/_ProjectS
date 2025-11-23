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
    [SerializeField] private List<GameObject> _level2HeroList; // 레벨2 영웅 프리팹 리스트 
    public List<Monster> heroList { get; private set; }

    public void Init()
    {
        // 리펙토링 : 영웅 리스트인데 왜 몬스터를 담고 있는지?? 
        heroList = new List<Monster>();
    }

    // 리펙토링 : 중복코드 제거 
    public void SpawnHero(Vector3 position, float speed = 0)
    {
        int randomNumber = Random.Range(0, (int)HeroType.Count);
        HeroType heroType = (HeroType)randomNumber;

        switch (heroType)
        {
            case HeroType.Warrior:
                GameObject spawnHero = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero = spawnHero.GetComponent<Hero>();
                hero.Init(speed);
                break;

            case HeroType.Archer:
                GameObject spawnHero1 = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero1 = spawnHero1.GetComponent<Hero>();
                hero1.Init(speed);
                break;

            case HeroType.Mage:
                GameObject spawnHero2 = Instantiate(_heroPrefabList[randomNumber], position, Quaternion.identity);
                Hero hero2 = spawnHero2.GetComponent<Hero>();
                hero2.Init(speed);
                break;
        }
    }

    public void SpawnHero()
    {

    }

    public void Remove(GameObject hero)
    {
        Destroy(hero);
    }

    public void Promotion(Vector3 position)
    {
        int randomNumber = Random.Range(0, (int)HeroType.Count);
        HeroType heroType = (HeroType)randomNumber;

        GameObject promotionHero = Instantiate(_level2HeroList[randomNumber], position, Quaternion.identity);
        Hero hero = promotionHero.GetComponent<Hero>();
        hero.Init();
    }
}
