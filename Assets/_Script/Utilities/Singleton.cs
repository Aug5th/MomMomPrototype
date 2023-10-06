using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticInstance<T> : MyMonoBehaviour where T : MyMonoBehaviour
{
    public static T Instance { get; private set; }
    protected override void Awake()
    {
        Instance = this as T;
        base.Awake();
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public abstract class Singleton<T> : StaticInstance<T> where T : MyMonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }
}

public abstract class PresistentSingleton<T> : Singleton<T> where T : MyMonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}



