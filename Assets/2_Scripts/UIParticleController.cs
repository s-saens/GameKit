using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticleController : MonoBehaviour
{
    [SerializeField] private Transform inheritTarget;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float multiplier = 0.15f;

    void Update()
    {
        particle.transform.localScale = inheritTarget.localScale * multiplier;
    }

    private void OnEnable()
    {
        particle.Play();
    }
}
