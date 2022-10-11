using System.Collections;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Tween.Wait(2).Then(LoadScene));
    }

    private void LoadScene()
    { 
        if(GameData.isNewbie.value)
            StartCoroutine(Tween.Wait(0.5f).Then(()=>SceneController.Instance.LoadScene(SceneEnum.Tutorial)));
        else if(GameData.wasPlaying.value)
            SceneController.Instance.LoadScene(SceneEnum.Game);
        else
            SceneController.Instance.LoadScene(SceneEnum.Home); 
    }
}
