using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewToggleRemote : MonoBehaviour
{
    [SerializeField] private GameObject view;

    private void OnEnable()
    {
        view.SetActive(true);
    }

    private void OnDisable()
    {
        view.SetActive(false);
    }
}
