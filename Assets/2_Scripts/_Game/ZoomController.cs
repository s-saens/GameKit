using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [SerializeField] private EventFloat pinchEvent_;
    [SerializeField] private float zoomStrength = 2;


    private void OnEnable()
    {
        pinchEvent_.callback += Zoom;
    }
    private void OnDisable()
    {
        pinchEvent_.callback -= Zoom;
    }
    
    private void Zoom(float v) // positive: zoom in
    {
        if(CameraOrthSizeController.onTransition || GameData.isBallSinking) return;

        Camera cam = GameSceneObjects.Instance.cam;
        float to = cam.orthographicSize - v * 60 * Time.deltaTime * (GameData.camOrthMax - GameData.camOrthMin) * zoomStrength;

        if(to > GameData.camOrthMax) to = GameData.camOrthMax;
        else if(to < GameData.camOrthMin) to = GameData.camOrthMin;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, to, 0.8f);
        GameData.camOrthSize.value = cam.orthographicSize;
    }
}