using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private Event deathEvent_;

    private void OnEnable()
    {
        deathEvent_.callback += OnDeath;
    }
    private void OnDisable()
    {
        deathEvent_.callback -= OnDeath;
    }

    [SerializeField] private Popup deathPopup;
    [SerializeField] private Popup resurrectionPopup;

    private void OnDeath()
    {
        PlaySound();

        
        SaveTemporaryPrefs();

        if(GameData.remainReviveChance.value == 0) PopupController.Instance.Open(deathPopup);
        else PopupController.Instance.Open(resurrectionPopup);
        
        DeletePrefs();
    }

    [SerializeField] private AudioSource deathSound;

    private void PlaySound()
    {
        deathSound.Play();
    }

    private void SaveTemporaryPrefs()
    {
        GameData.wasPlaying.value = false;
        GameData.Temporary.lastMaze = GameData.Last.maze.value;
        GameData.Temporary.lastPosition = GameData.Last.position.value;
        GameData.Temporary.lastFloor = GameData.Last.floor.value;
    }

    private void DeletePrefs()
    {
        GameData.Last.floor.LockEvent();
        GameData.Last.floor.DeletePrefs();
        GameData.Last.floor.UnlockEvent();
    }
}