using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Popup tutorialPopup;

    private void Start()
    {
        PopupController.Instance.Open(tutorialPopup);
    }
}
