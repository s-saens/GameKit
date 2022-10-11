using UnityEngine;

public class CameraOrthSizeController : MonoBehaviour
{
    [SerializeField] private EventFloat camOrthSizeEvent_;
    public static bool onTransition = false;

    private void OnEnable()
    {
        camOrthSizeEvent_.callback += OrthSizeTo;
    }
    private void OnDisable()
    {
        camOrthSizeEvent_.callback -= OrthSizeTo;
    }
    
    private void OrthSizeTo(float to)
    {
        StopAllCoroutines();
        Camera cam = GameSceneObjects.Instance.cam;
        onTransition = true;
        StartCoroutine(
            cam.orthographicSize.To_Lerp(to, 0.2f, (v) => cam.orthographicSize = v)
            .Then(()=>onTransition = false)
        );
    }
}