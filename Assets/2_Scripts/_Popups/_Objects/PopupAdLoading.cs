using UnityEngine;
using System.Collections;

public class PopupAdLoading : Popup
{
    [SerializeField] private Event adShowStartEvent_;
    [SerializeField] private Popup adFailPopup;
    private const float timeout = 10;
    protected override void _Back() {}

    protected override void WhenOpen()
    {
        adShowStartEvent_.callback += PopupController.Instance.Close;

        DetectTimeOut();
    }
    protected override void WhenClose()
    {
        adShowStartEvent_.callback -= PopupController.Instance.Close;
    }

    private void DetectTimeOut()
    {
        StartCoroutine(
            Tween.Wait(timeout, true)
            .Then(() => OpenFailPopup())
        );
    }

    private void OpenFailPopup()
    {
        if(!gameObject.activeSelf) PopupController.Instance.Close();
        PopupController.Instance.Close();
        PopupController.Instance.Open(adFailPopup);
    }

    public override IEnumerator TransitionClose()
    {
        yield return 0;
    }
}