using UnityEngine;

public class GravityKeyboardInput : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private EventVector2 gravityEvent;

    private float gravityX = 0;
    private float gravityY = 0;

    // void Update()
    // {
    //     gravityX = 0;
    //     gravityY = 0;
    //     if(Input.GetKey(KeyCode.A)) gravityX = -1;
    //     if(Input.GetKey(KeyCode.S)) gravityY = -1;
    //     if(Input.GetKey(KeyCode.D)) gravityX = 1;
    //     if(Input.GetKey(KeyCode.W)) gravityY = 1f;

    //     gravityEvent.Invoke(new Vector2(gravityX, gravityY) * 0.2f);
    // }
#endif
}