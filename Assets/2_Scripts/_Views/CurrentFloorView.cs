using UnityEngine;
using TMPro;

public class CurrentFloorView : DataTextView<int>
{
    protected override Data<int> data {
        get {
            return GameData.Last.floor;
        }
    }
}