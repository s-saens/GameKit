using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolToggle : MonoBehaviour
{
    [SerializeField] private GameObject ifTrue;
    [SerializeField] private GameObject ifFalse;

    protected virtual Data<bool> toggler {
        get;
    }

    private void OnEnable()
    {
        Toggle(toggler.value);
        toggler.onChange += Toggle;
    }
    private void OnDisable()
    {
        toggler.onChange -= Toggle;
    }

    private void Toggle(bool purchased)
    {
        if(ifTrue != null) ifTrue.SetActive(purchased);
        if(ifFalse != null) ifFalse.SetActive(!purchased);
    }
}
