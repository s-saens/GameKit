using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInvincibilityController : MonoBehaviour
{
    [SerializeField] private EventFloat invincibilityEvent;
    [SerializeField] private float time = 2.5f;

    private void Start()
    {
        GameData.isInvincible = true;
        invincibilityEvent.Invoke(time);
    }
}
