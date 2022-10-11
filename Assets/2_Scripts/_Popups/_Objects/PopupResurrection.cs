using UnityEngine;

public class PopupResurrection : Popup
{
    [SerializeField] private Event adCompletedEvent_;
    [SerializeField] private Event resurrectionEvent;
    protected override void WhenOpen()
    {
        adCompletedEvent_.callback += OnAdCompleted;
    }
    protected override void WhenClose()
    {
        adCompletedEvent_.callback -= OnAdCompleted;
    }

    protected override void _Back()
    {
        // nothing
    }

    private void OnAdCompleted()
    {
        PopupController.Instance.CloseAll();
        resurrectionEvent.Invoke();
    }
}