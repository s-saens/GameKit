using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeStartAnimationController : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private float size = 1;

    private void Awake()
    {
        container.localScale = Vector3.zero;
    }
    
    private void Start()
    {
        StartCoroutine(
            container.localScale.To_Lerp(Vector3.one * size, 0.1f, (v) => container.localScale = v)
        );
    }
}
