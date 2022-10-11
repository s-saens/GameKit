using UnityEngine;

public class SButtonFloat : SButton<float>
{
    [SerializeField] private EventFloat clickEventFloat;
    private void Awake()
    {
        clickEvent = clickEventFloat;
    }
}