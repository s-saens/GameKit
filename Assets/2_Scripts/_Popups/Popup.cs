using UnityEngine;
using System.Collections;

public enum PopupType
{
    Menu = 0,
    Death,
    DeathConfirm,
    HomeConfirm,
    AdLoadingFailed,
}

public class Popup : MonoBehaviour
{
    [SerializeField] private bool back = true;
    [SerializeField] private bool bgClick = false;
    [SerializeField] protected bool stopTime = false;
    [SerializeField] public float alpha = 0.9f;


    public bool IsOn
    {
        get;
        protected set;
    }


    protected RectTransform rect;
    private RectTransform canvasRect;

    private void Awake()
    {
        canvasRect = transform.parent.GetComponent<RectTransform>();
    }

    public void ReOpen()
    {
        _Open();
        StartCoroutine(TransitionReOpen().Then(AfterOpen));
    }

    public void Open()
    {
        _Open();
        StartCoroutine(TransitionOpen().Then(AfterOpen));
    }

    protected virtual void _Open()
    {
        rect = this.GetComponent<RectTransform>();
        GameData.backEvent.callback += Back;
        GameData.bgClickEvent.callback += OnClickBG;

        gameObject.SetActive(true);

        StopAllCoroutines();

        IsOn = true;

        WhenOpen();
        if (stopTime) Time.timeScale = 0;
    }


    public virtual void Close()
    {
        GameData.backEvent.callback -= Back;
        GameData.bgClickEvent.callback -= OnClickBG;

        StopAllCoroutines();

        if(!IsOn) return;

        IsOn = false;
        WhenClose();
        if (stopTime) Time.timeScale = 1;
        StartCoroutine(TransitionClose().Then(AfterClose).Then(() => gameObject.SetActive(false)));
    }

    private void OnClickBG()
    {
        if(bgClick) _OnClickBG();
    }
    protected virtual void _OnClickBG()
    {
        _Back();
    }
    private void Back()
    {
        if(back) _Back();
    }
    protected virtual void _Back()
    {
        PopupController.Instance.Close();
    }

    protected virtual void WhenOpen() {}
    protected virtual void AfterOpen() {}
    protected virtual void WhenClose() {}
    protected virtual void AfterClose() {}

    public virtual IEnumerator TransitionReOpen()
    {
        yield return TransitionOpen();
    }

    private Vector2 start {
        get {
            return Vector2.up * canvasRect.sizeDelta.y;
        } 
    }

    public virtual IEnumerator TransitionOpen()
    {
        rect.anchoredPosition = start;

        yield return rect.anchoredPosition.To_Lerp(
            Vector2.zero,
            0.3f,
            (v) => rect.anchoredPosition = v,
            true
        );
    }


    public virtual IEnumerator TransitionClose()
    {
        Vector2 s = start;
        yield return rect.anchoredPosition.To_Lerp(
            s,
            0.3f,
            (v) => rect.anchoredPosition = v,
            true
        );
    }
}