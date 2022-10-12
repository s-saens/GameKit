using UnityEngine;

public class SButtonBool : SButton<bool>
{
    [SerializeField] private EventBool clickEventBool;
    private void Awake()
    {
        clickEvent = clickEventBool;
    }
}