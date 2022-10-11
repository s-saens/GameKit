using UnityEngine;
using System.Collections;

public class PopupAdLoadingFailed : Popup
{
    [SerializeField] private float duration = 1;

    protected override void WhenOpen()
    {
        StartCoroutine(
            Tween.Wait(1, stopTime)
            .Then(() => _Back())
        );
    }

    public override IEnumerator TransitionOpen()
    {
        rect.anchoredPosition = Vector2.zero;
        yield return 0;
    }
}