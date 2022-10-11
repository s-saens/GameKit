using UnityEngine;
using TMPro;


public class RemainChanceView : DataTextView<int>
{
    protected override Data<int> data {
        get {
            return GameData.remainReviveChance;
        }
    }
}
