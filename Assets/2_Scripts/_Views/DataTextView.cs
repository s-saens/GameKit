using UnityEngine;
using TMPro;

public class DataTextView<T> : MonoBehaviour
{
    [SerializeField] private string prefix = "";
    [SerializeField] private string suffix = "";
    [SerializeField] private TMP_Text text;

    protected virtual Data<T> data {
        get;
    }

    private void OnEnable()
    {
        Init();
        OnChange(data.value);
        data.onChange += OnChange;
    }
    private void Init()
    {
        if(text == null) text = GetComponent<TMP_Text>();
    }
    private void OnDisable()
    {
        data.onChange -= OnChange;
    }
    
    private void OnChange(T value)
    {
        text.text = $"{prefix}{value.ToString()}{suffix}";
    }
}