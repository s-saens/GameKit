using UnityEngine;
using Newtonsoft.Json;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private Event completeEvent;

    private bool isTriggered = false;

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Ball" && !isTriggered)
        {
            completeEvent.Invoke();
            isTriggered = true;
        }
    }
}