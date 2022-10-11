using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SEController : SingletonMono<SEController>
{
    [SerializeField] private GameObject sourcePrefab;
    [SerializeField] private int poolCount = 5;
    private List<AudioSource> sources = new List<AudioSource>();

    private void Start()
    {
        InstantiateSource();
    }

    public void Play(AudioClip seClip, float seVolume = 1, float sePitch = 1)
    {
        int nowIndex = 0;
        while(nowIndex < sources.Count && sources[nowIndex].clip != seClip && sources[nowIndex].isPlaying) nowIndex++;

        if(nowIndex >= sources.Count) InstantiateSource();

        AudioSource s = sources[nowIndex];

        if(s.clip != seClip) s.clip = seClip;

        s.volume = seVolume;
        s.pitch = sePitch;
        s.Play();
    }

    private void InstantiateSource()
    {
        for(int i=0 ; i<poolCount ; ++i) sources.Add(Instantiate(sourcePrefab, transform).GetComponent<AudioSource>());
    }
}