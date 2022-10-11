using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackController : MonoBehaviour
{
    private void OnEnable()
    {
        GameData.backEvent.callback += OnBack;
    }

    private void OnDisable()
    {
        GameData.backEvent.callback -= OnBack;
    }

    [SerializeField] private Popup menuPopup;

    private void OnBack()
    {
        PopupController pc = PopupController.Instance;
        if(pc.stackCount == 0) pc.Open(menuPopup);
    }
}