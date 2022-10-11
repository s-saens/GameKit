using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPositionToggleController : OnOffToggleController
{
    protected override bool isOn {
        get {
            return GameData.Settings.joystickPosition.value;
        }
    }
    
    protected override void Toggle()
    {
        GameData.Settings.joystickPosition.value = !isOn;
        base.Toggle();
    }
}
