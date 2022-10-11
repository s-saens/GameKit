using UnityEngine;
using UnityEngine.EventSystems;

public class BackInput : SingletonMono<BackInput>
{
    public static bool canBack = true;
    [SerializeField] private AudioClip backSound;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameData.backEvent.Invoke();
        }
    }
}
