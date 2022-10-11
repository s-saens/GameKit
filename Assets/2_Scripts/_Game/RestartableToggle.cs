using UnityEngine;

public class RestartableToggle : MonoBehaviour
{
    [SerializeField] private int minimum = 6;

    [SerializeField] private GameObject o;
    [SerializeField] private GameObject x;


    private void OnEnable()
    {
        Toggle(GameData.highestFloor.value);
        GameData.highestFloor.onChange += Toggle;
    }
    private void OnDisable()
    {
        GameData.highestFloor.onChange -= Toggle;
    }

    private void Toggle(int hf)
    {
        bool isO = hf >= minimum;
        if(o != null) o.SetActive(isO);
        if(x != null) x.SetActive(!isO);
    }
}