using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PopupTutorial : Popup
{
    [SerializeField] private float timeOut = 2;
    [SerializeField] private AudioClip seClip;

    protected override void _Back()
    {
        PopupController pc = PopupController.Instance;
        if(pc.stackCount > 1) pc.Close();
    }

    protected override void WhenOpen()
    {
        if(timeOut > 0) StartCoroutine(Tween.Wait(timeOut).Then(MoveScene));
    }
    private void MoveScene()
    {
        PopupController.Instance.CloseAll();
        SEController.Instance.Play(seClip, 0.5f);
        StartCoroutine(Tween.Wait(0.3f).Then(()=>SceneController.Instance.LoadScene(SceneEnum.Home)));
    }

    public override IEnumerator TransitionOpen()
    {
        yield return Transition(true);
    }
    public override IEnumerator TransitionClose()
    {
        yield return Transition(false);
    }


    private IEnumerator Transition(bool on)
    {
        if (on) rect.localScale = Vector3.zero;
        Vector3 to = on ? Vector3.one : Vector3.zero;
        yield return rect.localScale.To_Lerp(to, 0.2f, (v) => rect.localScale = v);
    }
}