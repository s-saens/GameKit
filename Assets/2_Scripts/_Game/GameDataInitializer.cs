using UnityEngine;

public class GameDataInitializer : MonoBehaviour
{
    private void Start()
    {
        GameData.isBallSinking = false;
        GameData.highestFloor.value = Mathf.Max(GameData.Last.floor.value, GameData.highestFloor.value);
        GameData.isNewbie.value = false;
    }
}