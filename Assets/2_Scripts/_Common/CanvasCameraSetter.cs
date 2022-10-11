using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCameraSetter : MonoBehaviour
{
    [SerializeField] private bool isSingleton = false;
    private void Awake()
    {
        Set();
        if(isSingleton) SceneManager.sceneLoaded += (a, b) => Set();
    }
    private void Set()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}