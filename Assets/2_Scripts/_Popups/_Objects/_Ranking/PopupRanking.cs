using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRanking : Popup
{
    protected override void _OnClickBG()
    {
        GameData.backEvent.Invoke();
    }
}