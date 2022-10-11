using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPToggle_remove_ads_restart : BoolToggle
{
    protected override Data<bool> toggler {
        get {
            return GameData.IAP.non_consumable[KeyData.IAP_remove_ads_restart];
        }
    }
}
