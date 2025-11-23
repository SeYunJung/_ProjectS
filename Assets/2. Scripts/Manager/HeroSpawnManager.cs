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
    [SerializeField] private List<GameObject> _level3HeroList;
    [SerializeField] private List<GameObject> _level4HeroList;
    [SerializeField] private List<GameObject> _level5HeroList;
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

    public void Promotion(Hero hero, Vector3 position)
    {
        hero.SetLevel(1);

        int randomNumber = Random.Range(0, (int)HeroType.Count);
        HeroType heroType = (HeroType)randomNumber;

        switch (hero.level)
        {
            case 2:
                GameObject promotionHero2 = Instantiate(_level2HeroList[randomNumber], position, Quaternion.identity);
                Hero hero2 = promotionHero2.GetComponent<Hero>();
                hero2.SetLevel(2);
                hero2.Init();
                break;
            case 3:
                GameObject promotionHero3 = Instantiate(_level3HeroList[randomNumber], position, Quaternion.identity);
                Hero hero3 = promotionHero3.GetComponent<Hero>();
                hero3.SetLevel(3);
                hero3.Init();
                break;
            case 4:
                GameObject promotionHero4 = Instantiate(_level4HeroList[randomNumber], position, Quaternion.identity);
                Hero hero4 = promotionHero4.GetComponent<Hero>();
                hero4.SetLevel(4);
                hero4.Init();
                break;
        }
    }

    public void HeroAwake(Hero hero, Vector3 position)
    {
        int randomNumber = Random.Range(0, (int)HeroType.Count);
        HeroType heroType = (HeroType)randomNumber;

        GameObject promotionHero = Instantiate(_level5HeroList[randomNumber], position, Quaternion.identity);
        Hero hero1 = promotionHero.GetComponent<Hero>();
        hero1.Init();
    }
}
