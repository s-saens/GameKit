using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SButton<T> : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip seClip;
    [SerializeField] private float seVolume = 1;
    [SerializeField] private T parameter;
    [SerializeField] private float delay = 0;
    private bool isDelaying = false;
    protected Event<T> clickEvent;
    public void OnPointerClick(PointerEventData e)
    {
        StopAllCoroutines();
        isDelaying = true;
        StartCoroutine(
            Tween.Wait(delay, true)
            .Then(()=>{
                isDelaying = false;
                clickEvent?.Invoke(parameter);
                if (seClip == null) Debug.Log("No clip was assigned to button " + this.name);
                else SEController.Instance.Play(seClip, seVolume);
            })
        );
    }
}

public class SButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip seClip;
    [SerializeField] private float seVolume = 1;
    [SerializeField] protected Event clickEvent;
    [SerializeField] private float delay = 0;
    private bool isDelaying = false;
    public void OnPointerClick(PointerEventData e)
    {
        StopAllCoroutines();
        isDelaying = true;
        StartCoroutine(
            Tween.Wait(delay, true)
            .Then(() =>
            {
                clickEvent?.Invoke();
                if(seClip == null) Debug.Log("No clip was assigned to button " + this.name);
                else SEController.Instance.Play(seClip, seVolume);
            })
        );
    }
}