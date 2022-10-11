using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip startSound;
    [SerializeField] private float volume = 1;
    [SerializeField] private float pitch = 1;
    [SerializeField] private float delay = 0;

    private void Start()
    {
        StartCoroutine(Tween.Wait(delay).Then(() => SEController.Instance.Play(startSound, volume, pitch)));
    }
}
