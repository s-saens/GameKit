using UnityEngine;
using UnityEngine.Audio;

public class AudioController : SingletonMono<AudioController>
{
    public AudioMixer mixer;

    [SerializeField] private EventString setVolumeEvent_;
    [SerializeField] private EventString setVolumePrefsEvent_;
    private void OnEnable()
    {
        foreach(var v in GameData.Settings.volumes) v.Value.onChange += ((volume)=>SetVolume(v.Key + ',' + volume.ToString()));
        setVolumeEvent_.callback += SetVolume;
        setVolumePrefsEvent_.callback += SetVolumePrefs;
    }
    private void OnDisable()
    {
        setVolumeEvent_.callback -= SetVolume;
        setVolumePrefsEvent_.callback -= SetVolumePrefs;
        foreach(var v in GameData.Settings.volumes) v.Value.onChange -= ((volume)=>SetVolume(v.Key + ',' + volume.ToString()));
    }

    private void Start()
    {
        SetVolumeAsPrefs();
    }

    public void SetVolumeAsPrefs()
    {
        foreach(var v in GameData.Settings.volumes) SetVolume(v.Key + ',' + v.Value.value.ToString());
    }

    public void SetVolume(string parameter)
    {
        string[] p = parameter.Split(",");
        string group = p[0];
        float volume = float.Parse(p[1]);
        volume = volume == 0 ? -80 : Mathf.Log10(volume) * 20;

        mixer.SetFloat(group, volume);
    }

    public void SetVolumePrefs(string parameter)
    {
        string[] p = parameter.Split(",");

        string group = p[0];
        float volume = float.Parse(p[1]);

        GameData.Settings.volumes[group].value = volume;
    }
}