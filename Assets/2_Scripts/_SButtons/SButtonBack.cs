using UnityEngine;

public class SButtonBack : SButton
{
    private void Awake()
    {
        clickEvent = GameData.backEvent;
    }
}