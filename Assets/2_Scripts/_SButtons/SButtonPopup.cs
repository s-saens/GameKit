using UnityEngine;

public class SButtonPopup : SButton<Popup>
{
    [SerializeField] private EventPopup clickEventPopup;
    private void Awake()
    {
        clickEvent = clickEventPopup;
    }
}