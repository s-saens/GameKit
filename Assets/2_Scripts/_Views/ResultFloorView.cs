using UnityEngine;
using TMPro;

public class ResultFloorView : MonoBehaviour
{
    [SerializeField] private string prefix = "";
    [SerializeField] private TMP_Text floorText;

    private void OnEnable()
    {
        FloorData(GameData.Temporary.lastFloor);
    }
    
    private void FloorData(int floor)
    {
        floorText.text = $"{prefix}{floor.ToString()}F";
    }
}