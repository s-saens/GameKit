using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T Instance
    {
        get;
        private set;
    }

    protected void Awake()
    {
        Instance = this.GetComponent<T>();
    }
}