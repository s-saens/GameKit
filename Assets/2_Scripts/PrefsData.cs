using UnityEngine;

public class PrefsData<T> : Data<T>
{
    private string key;
    private T defaultValue;

    public override T value
    {
        get
        {
            return PlayerPrefsExt.GetObject<T>(key, defaultValue);
        }
        set
        {
            PlayerPrefsExt.SetObject<T>(key, value);
            if(!blockChangeEvent) this.onChange?.Invoke(value);
        }
    }

    public PrefsData(string key, T defaultValue = default(T))
    {
        this.key = key;
        this.defaultValue = defaultValue;
        if(!PlayerPrefs.HasKey(key)) this.value = defaultValue;
    }

    public void DeletePrefs()
    {
        this.value = defaultValue;
    }
}