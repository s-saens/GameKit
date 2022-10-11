using UnityEngine;

public class RetryFromHalfController : MonoBehaviour
{
    [SerializeField] private Event retryEvent_;

    private void OnEnable()
    {
        retryEvent_.callback += Retry;
    }
    private void OnDisable()
    {
        retryEvent_.callback -= Retry;
    }
    
    private void Retry()
    {
        GameData.Last.floor.value = GameData.savedFloor;
        GameData.remainReviveChance.value = GameData.IAP.non_consumable[KeyData.IAP_revive_chance].value ? 2 : 1;
        PopupController.Instance.CloseAll();
        SceneController.Instance.LoadScene(SceneEnum.Game);
    }
}
