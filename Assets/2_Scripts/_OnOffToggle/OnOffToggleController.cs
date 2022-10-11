using UnityEngine;

public class OnOffToggleController : MonoBehaviour
{
    [SerializeField] private Event toggleEvent;

    [SerializeField] protected GameObject on;
    [SerializeField] protected GameObject off;

    protected virtual bool isOn { get; }

    private void OnEnable()
    {
        toggleEvent.callback += Toggle;
        SetView();
    }
    private void OnDisable()
    {
        toggleEvent.callback -= Toggle;
    }

    private void SetView()
    {
        on.SetActive(!isOn);
        off.SetActive(isOn);
    }
    
    protected virtual void Toggle()
    {
        SetView();
    }
}