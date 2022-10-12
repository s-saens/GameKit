using UnityEngine;

public class SButtonString : SButton<string>
{
    [SerializeField] private EventString clickEventString;
    private void Awake()
    {
        clickEvent = clickEventString;
    }
}