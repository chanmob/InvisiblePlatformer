using UnityEngine;

[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static volatile T _instance;
    private static object _syncRoot = new System.Object();

    public static T instance {
        get {
            Initialize();
            return _instance;
        }
    }

    public static bool isInitialized {
        get {
            return _instance != null;
        }
    }

    public static void Initialize()
    {
        if (_instance != null)
        {
            return;
        }
        lock (_syncRoot)
        {
            _instance = GameObject.FindObjectOfType<T>();

            if(_instance == null)
            {
                var go = new GameObject(typeof(T).FullName);
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<T>();
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError(GetType().Name + " 싱글톤 클래스 존재");
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
