using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null) return instance;

            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
            }

            return instance;
        }
    }

    protected void Awake()
    {
        instance = this as T;
        Init();
    }

    public virtual void Init()
    {

    }
}
