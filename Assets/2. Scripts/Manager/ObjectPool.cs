using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // 풀 정보 클래스. 유니티에서 보이기 위해 Serializable 사용 
    [System.Serializable]
    public class Pool
    {
        public string tag; // 생성할 오브젝트 이름(태그)
        public GameObject prefab; // 생성할 오브젝트 프리팹
        public int size; // 생성할 오브젝트 개수 
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    [SerializeField] private Transform _projectileSpawnPoint;

    #region Singleton
    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void Init()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, _projectileSpawnPoint);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // 예외 처리 
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // 재사용하기 위해 
        _poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
