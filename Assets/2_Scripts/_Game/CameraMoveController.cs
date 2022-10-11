using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [SerializeField] private EventVector3 moveCamEvent_;
    [SerializeField] private EventVector2 dragEvent_;
    [SerializeField] private EventFloat dragEndEvent_; 

    private Vector2 lastDirection;

    private bool dragging = false;

    private void OnEnable()
    {
        moveCamEvent_.callback += MoveTo;
        dragEvent_.callback += Move;
        dragEndEvent_.callback += DoInertia;
    }
    private void OnDisable()
    {
        moveCamEvent_.callback -= MoveTo;
        dragEvent_.callback -= Move;
        dragEndEvent_.callback -= DoInertia;
    }

    private void MoveTo(Vector3 to)
    {
        StopAllCoroutines();
        
        to = new Vector3(to.x, to.y, this.transform.position.z);
        
        StartCoroutine(
            this.transform.position.To_Lerp(to, 0.1f, (v) => this.transform.position = v)
        );
    }

#region CAM DRAG
    [Header("CAM DRAG")]
    [SerializeField] private float strength = 3;
    private void Move(Vector2 direction)
    {
        StopAllCoroutines();
        if(GameData.isBallSinking) return;
        
        dragging = true;
        direction *= 0.005f * strength;
#if !UNITY_EDITOR
        direction /= Input.touchCount * Input.touchCount;
#endif
        lastDirection = -direction;
        GameSceneObjects.Instance.cam.transform.Translate(-direction);
    }
#endregion

#region INERTIA - DRAG END
    private void DoInertia(float lastVelocity)
    {
        StartCoroutine(InertiaCoroutine(lastVelocity));
    }

    private IEnumerator InertiaCoroutine(float lastVelocity)
    {
        lastVelocity *= 3f / Screen.width;
        lastDirection = lastDirection.normalized;
        while(lastVelocity > 0.0001f)
        {
            GameSceneObjects.Instance.cam.transform.Translate(lastDirection * lastVelocity * Time.deltaTime * 60);
            lastVelocity *= 0.89f;
            yield return 0;
        }
    }
#endregion
}