using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json;

public class NowLocationInput : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private EventVector3 moveCamEvent;
    [SerializeField] private float delay = 0.3f;
    [SerializeField] private float area = 100;

    private bool isCounting = false;
    private int count = 1;

    Vector2 lastPoint = Vector2.zero;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isCounting) count++;

        bool canInvoke = Vector2.Distance(lastPoint, eventData.position) <= area;

        lastPoint = eventData.position;

        if(count == 2 && canInvoke) moveCamEvent?.Invoke(GameSceneObjects.Instance.ballRigid.position);

        isCounting = true;
        StopAllCoroutines();
        StartCoroutine(
            Tween.Wait(delay).Then(() => {
                isCounting = false;
                count = 1;
            })
        );
    }
}
