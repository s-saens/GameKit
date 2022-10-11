using UnityEngine;

public class PopupDeathConfirm : Popup
{
    protected override void WhenOpen()
    {
        foreach (var v in GameData.Settings.volumes) v.Value.LockEvent();
        AudioController.Instance.SetVolume("ME,0");
        AudioController.Instance.SetVolume("BGM,0.7");
    }
    protected override void WhenClose()
    {
        AudioController.Instance.SetVolume("ME," + GameData.Settings.volumes["ME"].value.ToString());
        AudioController.Instance.SetVolume("BGM," + GameData.Settings.volumes["BGM"].value.ToString());
        foreach (var v in GameData.Settings.volumes) v.Value.UnlockEvent();
    }
}