using UnityEngine;
using UnityEngine.EventSystems;

public class PinchInput : MonoBehaviour
{
    public EventFloat pinchEvent;

    private bool touchStarted = false;
    private float lastDistance = 0;

    private void Update()
    {
        if(Input.touchCount != 2)
        {
            touchStarted = false;
            lastDistance = 0;
            return;
        }

        Touch t1 = Input.touches[0];
        Touch t2 = Input.touches[1];

        float nowDistance = Vector2.Distance(t1.position, t2.position) / 1920;

        if(!touchStarted)
        {
            lastDistance = nowDistance;
            touchStarted = true;
            return;
        }

        float deltaDistance = nowDistance - lastDistance;

        if(deltaDistance > 0.001f) // 두 점 사이가 멀어짐 : 축소
        {
            pinchEvent.Invoke(deltaDistance);
        }
        else if(deltaDistance < -0.001f) // 두 점 사이가 가까워짐 : 확대
        {
            pinchEvent.Invoke(deltaDistance);
        }

        lastDistance = nowDistance;
    }
}
