using UnityEngine;

public class StartAnimationController : MonoBehaviour
{
    [SerializeField] private EventFloat camOrthSizeEvent;

    private void Start()
    {
        StartAnimation();
        SetCamPosition();
    }

    private void StartAnimation()
    {
        float nowOrthSize = GameData.camOrthSize.value;
        camOrthSizeEvent.Invoke(nowOrthSize > GameData.camOrthMax ? GameData.camOrthMax : nowOrthSize);
    }

    private void SetCamPosition()
    {
        Vector2 ballPos = GameSceneObjects.Instance.ball.transform.position;
        Transform camTransform = GameSceneObjects.Instance.cam.transform;
        camTransform.SetPositionXY(ballPos.x, ballPos.y);
    }
}