using UnityEngine;

public class SButtonBG : SButton
{
    private void Awake()
    {
        clickEvent = GameData.bgClickEvent;
    }
}