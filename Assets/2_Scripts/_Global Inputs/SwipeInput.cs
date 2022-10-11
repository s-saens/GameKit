using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeInput : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Event swipeStartEvent;
    public EventFloat swipingEvent;
    public EventFloat swipeEndEvent;

    private float firstX;

    public void OnInitializePotentialDrag(PointerEventData e)
    {
        swipeStartEvent.Invoke();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        firstX = e.position.x;
    }

    public void OnDrag(PointerEventData e)
    {
        swipingEvent.Invoke(e.delta.x * Time.deltaTime * 60);
    }

    public void OnEndDrag(PointerEventData e)
    {
        swipeEndEvent.Invoke(e.delta.x * Time.deltaTime * 60);
    }

}
