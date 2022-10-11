using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSettings : Popup
{
    protected override void _OnClickBG()
    {
        GameData.backEvent.Invoke();
    }
    protected override void WhenOpen()
    {
        AudioController.Instance.SetVolume("ME,0");
    }
    protected override void WhenClose()
    {
        AudioController.Instance.SetVolume("ME," + GameData.Settings.volumes["ME"].value.ToString());
    }
}