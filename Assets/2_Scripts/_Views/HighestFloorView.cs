using UnityEngine;
using TMPro;

public class HighestFloorView : DataTextView<int>
{
    protected override Data<int> data {
        get {
            return GameData.highestFloor;
        }
    }
}