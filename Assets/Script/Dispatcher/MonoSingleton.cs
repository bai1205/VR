using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (MonoSingletonObject.go == null)
            {
                MonoSingletonObject.go = new GameObject("MonoSingletonObject");
                DontDestroyOnLoad(MonoSingletonObject.go);
            }

            if (MonoSingletonObject.go != null && instance == null)
            {
                instance = MonoSingletonObject.go.AddComponent<T>();
            }

            return instance;
        }
    }

    // Sometimes certain components are destroyed when the scene changes
    public static bool destroyOnLoad = false;

    // Add event listener for scene changes
    public void AddSceneChangedEvent()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        if (destroyOnLoad)
        {
            if (instance != null)
            {
                DestroyImmediate(instance); // Destroy immediately
                Debug.Log(instance == null);
            }
        }
    }
}

// Cache a GameObject
public class MonoSingletonObject
{
    public static GameObject go;
}
