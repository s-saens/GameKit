using UnityEngine;

public class AdFailController : MonoBehaviour
{
    [SerializeField] private Event adLoadFailEvent;
    [SerializeField] private Popup adLoadFailPopup;
    
    private void OnEnable()
    {
        adLoadFailEvent.callback += ShowFailPopup;
    }
    private void OnDisable()
    {
        adLoadFailEvent.callback -= ShowFailPopup;
    }

    private void ShowFailPopup()
    {
        PopupController.Instance.Open(adLoadFailPopup);
    }
}