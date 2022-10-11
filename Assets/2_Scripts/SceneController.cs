using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    Splash = 0,
    Home,
    Game,
    Tutorial
}

public class SceneController : SingletonMono<SceneController>
{
    [SerializeField] private EventInt loadSceneEvent_;
    private void OnEnable()
    {
        loadSceneEvent_.callback += LoadScene;
    }
    private void OnDisable()
    {
        loadSceneEvent_.callback -= LoadScene;
    }
    
    private void LoadScene(int si)
    {
        LoadScene((SceneEnum)si);
    }

    public void LoadScene(SceneEnum se)
    {
        PopupController.Instance.CloseAll();
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        StartCoroutine(SceneMoveCoroutine(loadOperation));
    }

    IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation)
    {
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
    }
}