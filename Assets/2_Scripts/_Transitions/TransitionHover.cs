using UnityEngine;
using UnityEngine.EventSystems;

public class TransitionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected virtual void OnHover(bool isHover) {}

    public void OnPointerEnter(PointerEventData e)
    {
        OnHover(true);
    }
    public void OnPointerExit(PointerEventData e)
    {
        OnHover(false);
    }
}