using UnityEngine;

public class RetryFromStartController : MonoBehaviour
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
        GameData.Last.floor.value = 1;
        PopupController.Instance.CloseAll();
        SceneController.Instance.LoadScene(SceneEnum.Game);
    }
}
