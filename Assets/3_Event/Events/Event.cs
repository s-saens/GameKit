using UnityEngine;
using System;

public abstract class Event<T> : ScriptableObject
{
    public Action<T> callback;
    
    public void Invoke(T param)
    {
        callback?.Invoke(param);
    }
}

// DEFAULT
[CreateAssetMenu(menuName = "Event/_Default", fileName="Event")]
public class Event : ScriptableObject
{
    public Action callback;

    public void Invoke()
    {
        callback?.Invoke();
    }
}