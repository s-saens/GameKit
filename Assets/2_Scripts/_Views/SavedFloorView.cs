using UnityEngine;
using TMPro;

public class SavedFloorView : MonoBehaviour
{
    [SerializeField] private string prefix = "";
    [SerializeField] private TMP_Text floorText;

    private void Start()
    {
        int saved = GameData.savedFloor;
        floorText.text = $"{prefix}{(saved).ToString()}F";
    }
}