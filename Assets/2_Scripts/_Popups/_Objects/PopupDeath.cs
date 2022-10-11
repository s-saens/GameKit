using UnityEngine;
using System.Collections;

public class PopupDeath : Popup
{
    [SerializeField] private Event adCompletedEvent_;
    [SerializeField] private Event retryFromHalfEvent;
    protected override void WhenOpen()
    {
        GameData.remainReviveChance.value = GameData.IAP.non_consumable[KeyData.IAP_revive_chance].value ? 2 : 1;
        adCompletedEvent_.callback += OnAdCompleted;
    }
    protected override void WhenClose()
    {
        adCompletedEvent_.callback -= OnAdCompleted;
    }

    private void OnAdCompleted() // Death에서 광고보면 반절부터 시작
    {
        retryFromHalfEvent.Invoke();
    }

    [SerializeField] private Transform title;
    [SerializeField] private float delayTitle;
    [SerializeField] private Transform result;
    [SerializeField] private float delayResult;
    [SerializeField] private Transform buttons;
    [SerializeField] private float delayButtons;

    private float from = - Screen.width * 2;
    private float to = 0;
    private bool isOpening = true;


    protected override void _OnClickBG()
    {
        if(!isOpening) return;

        StopAllCoroutines();
        SetAll(to);
    }

    public override IEnumerator TransitionReOpen()
    {
        yield return base.TransitionOpen();
    }
    public override IEnumerator TransitionOpen()
    {
        SetAll(from);

        isOpening = true;
        StartCoroutine(Setting(title, delayTitle));
        StartCoroutine(Setting(result, delayResult));
        StartCoroutine(Setting(buttons, delayButtons));
        
        yield return Tween.Wait(2).Then(() => isOpening = false);
    }

    private IEnumerator Setting(Transform t, float delay)
    {
        yield return Tween.Wait(delay, true);
        yield return from.To_Lerp(to, 0.2f, t.SetLocalPositionX);
    }

    private void SetAll(float value)
    {
        title.SetLocalPositionX(value);
        result.SetLocalPositionX(value);
        buttons.SetLocalPositionX(value);
    }
}