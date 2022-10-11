using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleUI : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private RectTransform rect;

    private void Start()
    {
        if(cam == null) cam = Camera.main;
        rect = this.GetComponent<RectTransform>();
        SetPosition();
    }
    private void SetPosition()
    {
        rect.position = rect.transform.position;
    }
}
