using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSetter : MonoBehaviour
{
    public const int targetFPS = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFPS;
    }
}
