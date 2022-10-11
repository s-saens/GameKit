using UnityEngine;

public class SButtonInt : SButton<int>
{
    [SerializeField] private EventInt clickEventInt;
    private void Awake()
    {
        clickEvent = clickEventInt;
    }
}