using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BGMController : SingletonMono<BGMController>
{
    private AudioSource bgmSource;
    public AudioClip[] bgmClips;
    private AudioClip lastClip = null;

    private void OnEnable()
    {
        bgmSource = this.GetComponent<AudioSource>();
        SceneManager.sceneLoaded += SetBGM;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetBGM;
    }

    private void SetBGM(Scene scene, LoadSceneMode mode)
    {
        int nowSceneIndex = scene.buildIndex;
        AudioClip nowClip = bgmClips[nowSceneIndex];

        if(lastClip == nowClip) return;
        lastClip = nowClip;

        if(bgmClips[nowSceneIndex] == null)
        {
            Debug.Log("No bgm was set of index " + nowSceneIndex);
            bgmSource.Stop();
            return;
        }

        bgmSource.clip = nowClip;
        bgmSource.Play();
    }
}