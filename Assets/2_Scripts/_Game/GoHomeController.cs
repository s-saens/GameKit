using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeController : MonoBehaviour
{
    [SerializeField] private Event goHomeEvent_;
    private void OnEnable()
    {
        goHomeEvent_.callback += GoHome;
    }
    private void OnDisable()
    {
        goHomeEvent_.callback -= GoHome;
    }

    [SerializeField] private Camera cam;
    [SerializeField] private float orthSize;
    
    private void GoHome()
    {
        PopupController.Instance.CloseAll();
        SceneController.Instance.LoadScene(SceneEnum.Home);
    }
}