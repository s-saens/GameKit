using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePrefsController : MonoBehaviour
{
    private void Awake()
    {
        GameData.wasPlaying.value = false;
        GameData.camOrthSize.DeletePrefs();
        GameData.Last.floor.DeletePrefs();
        GameData.remainReviveChance.value = GameData.IAP.non_consumable[KeyData.IAP_revive_chance].value ? 2 : 1;
    }
}
