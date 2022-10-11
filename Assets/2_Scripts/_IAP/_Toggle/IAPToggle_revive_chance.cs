using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPToggle_revive_chance : BoolToggle
{
    protected override Data<bool> toggler {
        get {
            return GameData.IAP.non_consumable[KeyData.IAP_revive_chance];
        }
    }
}
