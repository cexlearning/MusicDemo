
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    //private static bool _destroyed;
    private enum EDestroySt
    {
        E_DST_DESTROYED = 0,
        E_DST_DESTROYING,
        E_DST_ALIVE
    }
    private static EDestroySt _st = EDestroySt.E_DST_DESTROYED;

    public static T instance
    {
        get
        {
            return GetInstance();
        }
    }

	public static T GetInstance()
	{
		if (_instance == null && _st == EDestroySt.E_DST_DESTROYED)
		{
			System.Type typeFromHandle = typeof(T);
			_instance = (T)(FindObjectOfType(typeFromHandle));
			if (_instance == null)
			{
				GameObject gameObject = new GameObject(typeof(T).Name);
				_instance = gameObject.AddComponent<T>();
                if (_instance != null)
                {
                    GameObject gameObject2 = GameObject.Find("BootObj");
                    if (gameObject2 != null)
                    {
                        gameObject.transform.SetParent(gameObject2.transform);
                    }
                    _st = EDestroySt.E_DST_ALIVE;
                }
                else
                {
                    Destroy(gameObject);
                }
			}
            else
            {
                _st = EDestroySt.E_DST_ALIVE;
                DontDestroyOnLoad(_instance);
            }
		}
		return _st == EDestroySt.E_DST_ALIVE ? _instance : default(T); // return null reference while destroying
	}

	public static void DestroyInstance()
	{
        if (_st == EDestroySt.E_DST_ALIVE)
		{
			Destroy(_instance.gameObject);
            _st = EDestroySt.E_DST_DESTROYING;
        }
	}

	protected virtual void Awake()
	{
		if (_instance != null && _instance.gameObject != gameObject)
		{
			if (Application.isPlaying)
			{
				Destroy(gameObject);
			}
			else
			{
				DestroyImmediate(gameObject);
			}
            return;
		}
		DontDestroyOnLoad(gameObject);
		Init();
	}

	protected virtual void OnDestroy()
	{
		if (_instance != null && _instance.gameObject == base.gameObject)
        {
            UnInit();
            _instance = (T)((object)null);
            _st = EDestroySt.E_DST_DESTROYED;
		}
	}

	public static bool HasInstance()
	{
		return _st == EDestroySt.E_DST_ALIVE && _instance != null;
	}

	protected virtual void Init() {}
    protected virtual void UnInit() {}
}
