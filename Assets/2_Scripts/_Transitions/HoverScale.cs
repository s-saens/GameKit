using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScale : TransitionHover
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float strength = 1.3f;

    protected override void OnHover(bool isHover)
    {
        Transform t = targetTransform == null ? transform : targetTransform;
        float to = isHover ? strength : 1f;
        StopAllCoroutines();
        StartCoroutine(t.localScale.To_Lerp(
            Vector3.one * to,
            0.1f,
            (v) => t.localScale = v)
        );
    }
}