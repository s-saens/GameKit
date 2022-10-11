using UnityEngine;
using UnityEngine.UI;

public class ResurrectionController : MonoBehaviour
{
    [SerializeField] private Event resurrectionEvent_;

    private void OnEnable()
    {
        resurrectionEvent_.callback += Resurrect;
    }
    private void OnDisable()
    {
        resurrectionEvent_.callback -= Resurrect;
    }

    [SerializeField] private float invincibilityTime = 2.5f;
    [SerializeField] private AudioClip resurrectionSE;
    [SerializeField] private EventFloat invincibilityEvent;
    
    private void Resurrect()
    {
        PopupController.Instance.CloseAll();

        GameData.remainReviveChance.value--;

        SEController.Instance.Play(resurrectionSE);

        GameSceneObjects.Instance.ball.SetActive(true);
        LoadPrefs();
        
        invincibilityEvent.Invoke(invincibilityTime);
    }

    private void LoadPrefs()
    {
        GameData.wasPlaying.value = true;
        GameData.Last.maze.value = GameData.Temporary.lastMaze;
        GameData.Last.position.value = GameData.Temporary.lastPosition;
        GameData.Last.floor.value = GameData.Temporary.lastFloor;
    }
}