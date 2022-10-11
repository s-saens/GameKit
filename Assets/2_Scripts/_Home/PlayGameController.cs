using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameController : SceneTransition
{
    [SerializeField] private Event adCompletedEvent_;
    [SerializeField] private EventBool playGameEvent_;
    
    private void OnEnable()
    {
        adCompletedEvent_.callback += PlayFrom;
        playGameEvent_.callback += Play;
    }
    private void OnDisable()
    {
        adCompletedEvent_.callback -= PlayFrom;
        playGameEvent_.callback -= Play;
    }

    [SerializeField] RectTransform container;
    [SerializeField] private float delay = 20;
    [SerializeField] private float size = 20;
    [SerializeField] private RectTransform canvasRect;
    
    private void PlayFrom()
    {
        Play(false);
    }

    private void Play(bool fromStart)
    {
        GameData.Last.floor.value = fromStart ? 1 : GameData.savedFloor;

        float sizeMultiplier = (canvasRect.sizeDelta.x > canvasRect.sizeDelta.y)
            ? canvasRect.sizeDelta.x / 1080
            : canvasRect.sizeDelta.y / 1920;

        BackInput.canBack = false;
        StartCoroutine(
            container.localScale.To_Lerp(Vector3.one * size * sizeMultiplier, 0.1f, (v) => container.localScale = v)
        );
        StartCoroutine(
            Tween.Wait(delay)
            .Then(()=>SceneController.Instance.LoadScene(SceneEnum.Game))
            .Then(()=>BackInput.canBack = true)
        );
    }
}
