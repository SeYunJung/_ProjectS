using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour_Singleton<ResourceManager>
{
    public T CreateCharacter<T>(string prefabName, Vector2 position, Transform parent = null) where T : Object // T 타입이 Object야 함 
    {
        return CreatePathAndObject<T>(ResourcePath.CHARACTER, prefabName, position, parent);
    }

    public T CreateTilemap<T>(string prefabName, Vector2 position, Transform parent = null) where T : Object
    {
        return CreatePathAndObject<T>(ResourcePath.GRID, prefabName, position, parent);
    }

    public T CreateUI<T>(string prefabName, Vector2 Position, Transform parent = null) where T : Object
    {
        return CreatePathAndObject<T>(ResourcePath.UI, prefabName, Position, parent);
    }

    // 경로를 만들고, 프리팹 생성/반환 
    public T CreatePathAndObject<T>(string resourcePath, string prefabName, Vector2 position, Transform parent = null) where T : Object
    {
        string path = resourcePath + prefabName;
        return CreateObject<T>(path, position, parent);
    }

    public T[] CreatePathAndLoadResources<T>(string resourcePath) where T : Object
    {
        string path = resourcePath;
        return LoadResources<T>(path);
    }

    // 프리팹 오브젝트 생성 
    public T CreateObject<T>(string path, Vector2 position, Transform parent = null) where T : Object
    {
        T resource = LoadResource<T>(path);

        if (resource == null)
        {
            Debug.Log($"프리팹이 없습니다 : {path}");
            return default;
        }

        T obj = Instantiate(resource, position, Quaternion.identity, parent);
        return obj;
    }

    public T LoadResource<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T[] LoadResources<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
}
