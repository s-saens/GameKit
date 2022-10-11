using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppPauseController : MonoBehaviour
{
    [SerializeField] private Popup menuPopup;

    void OnApplicationPause(bool pauseStatus)
    {
        if(PopupController.Instance.stackCount < 1 && pauseStatus) PopupController.Instance.Open(menuPopup);
    }
}