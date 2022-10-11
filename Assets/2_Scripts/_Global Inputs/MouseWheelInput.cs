using UnityEngine;
using UnityEngine.EventSystems;

public class MouseWheelInput : MonoBehaviour, IScrollHandler
{
    public EventFloat zoomEvent;
    public void OnScroll(PointerEventData e)
    {
        float scrollDelta = Mathf.Sign(e.scrollDelta.y);
        zoomEvent.Invoke(scrollDelta * 0.1f);
    }
}