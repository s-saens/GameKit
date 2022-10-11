using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeToggleController : OnOffToggleController
{
    protected override bool isOn {
        get {
            return GameData.Settings.volumes["Master"].value > 0;
        }
    }
    
    protected override void Toggle()
    {
        GameData.Settings.volumes["Master"].value = isOn ? 0 : 1;
        base.Toggle();
    }
}
