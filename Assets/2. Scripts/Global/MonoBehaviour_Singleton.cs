using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour_Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    private void Awake()
    {
        instance = this as T;
    }
}
