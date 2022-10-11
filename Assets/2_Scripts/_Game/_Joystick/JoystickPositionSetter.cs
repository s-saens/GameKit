using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPositionSetter : MonoBehaviour
{
    [Header("LEFT")]
    [SerializeField] private Vector2 anchPosLeft;
    [SerializeField] private Vector2 pivotLeft;


    [Header("RIGHT")]
    [SerializeField] private Vector2 anchPosRight;
    [SerializeField] private Vector2 pivotRight;

    private RectTransform rect;

    private void OnEnable()
    {
        rect = (RectTransform)(this.transform);
        Set(GameData.Settings.joystickPosition.value);
        GameData.Settings.joystickPosition.onChange += Set;
    }
    private void OnDisable()
    {
        GameData.Settings.joystickPosition.onChange -= Set;
    }

    private void Set(bool isRight)
    {
        if(isRight)
        {
            rect.anchorMin = Vector2.right;
            rect.anchorMax = Vector2.right;
            rect.anchoredPosition = anchPosRight;
            rect.pivot = pivotRight;
        }
        else
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.zero;
            rect.anchoredPosition = anchPosLeft;
            rect.pivot = pivotLeft;
        }
    }

}
