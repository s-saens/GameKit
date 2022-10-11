using UnityEngine;
using System.Collections.Generic;

public class PopupController : SingletonMono<PopupController>
{
    [SerializeField] private EventPopup openPopupEvent_;
    [SerializeField] private Event closePopupEvent_;
    [SerializeField] private Event closeAllPopupEvent_;

    private void OnEnable()
    {
        openPopupEvent_.callback += Open;
        closePopupEvent_.callback += Close;
        closeAllPopupEvent_.callback += CloseAll;
    }
    private void OnDisable()
    {
        openPopupEvent_.callback -= Open;
        closePopupEvent_.callback -= Close;
        closeAllPopupEvent_.callback -= CloseAll;
    }

    [SerializeField] private BackGround background;

    public int stackCount {
        get {return history.Count;}
    }

    private Stack<Popup> history = new Stack<Popup>();

    public void Open(Popup popup)
    {
        background.Set(popup.alpha);
        if (history.Count > 0) history.Peek().Close();

        popup.Open();
        history.Push(popup);
    }

    public void Close()
    {
        if (stackCount <= 0) return;

        Popup top = history.Pop();
        top?.Close();

        if(history.Count <= 0)
        {
            background.Off();
            return;
        }

        top = history.Peek();
        if(top == null) return;

        background.Set(top.alpha);
        top?.ReOpen();
    }

    public void CloseAll()
    {
        if(stackCount <= 0) return;
        history.Peek().Close();
        background.Off();
        history.Clear();
    }
}