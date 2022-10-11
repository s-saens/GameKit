using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json;

public class DragInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private static int touchCount = 0;
    public EventVector2 dragEvent;
    public EventFloat dragEndEvent;

    public void OnPointerDown(PointerEventData e)
    {
        touchCount++;
    }
    public void OnDrag(PointerEventData e)
    {
        if(GameData.isBallSinking) return;
        dragEvent.Invoke(e.delta);
    }

    public void OnPointerUp(PointerEventData e)
    {
        touchCount--;
        if(GameData.isBallSinking) return;
        dragEndEvent.Invoke(e.delta.magnitude);
    }
}
