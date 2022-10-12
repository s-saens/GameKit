using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class Listener<T> : MonoBehaviour
{
    public Event<T> event_;
    public UnityEvent<T> operations;

    private void OnEnable()
    {
        event_.callback += operations.Invoke;
    }
    private void OnDisable()
    {
        event_.callback -= operations.Invoke;
    }
}

// DEFAULT
public class Listener : MonoBehaviour
{
    public Event event_;
    public UnityEvent operations;

    private void OnEnable()
    {
        event_.callback += operations.Invoke;
    }
    private void OnDisable()
    {
        event_.callback -= operations.Invoke;
    }
}