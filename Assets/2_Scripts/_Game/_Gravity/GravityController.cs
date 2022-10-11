using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private int strength_gyro = 20;
    [SerializeField] private int strength_joystick = 5;
    [SerializeField] private EventVector2 gravityEvent_;
    [SerializeField] private EventVector2 joystickEvent_;
    
    private void OnEnable()
    {
        // gravityEvent_.callback += ChangeGravity_Gyro;
        joystickEvent_.callback += ChangeGravity_Joystick;
    }
    private void OnDisable()
    {
        // gravityEvent_.callback -= ChangeGravity_Gyro;
        joystickEvent_.callback -= ChangeGravity_Joystick;
    }

    private void ChangeGravity_Gyro(Vector2 gravity)
    {
        Physics2D.gravity = gravity * strength_gyro * GameData.spaceSize;
    }

    private void ChangeGravity_Joystick(Vector2 gravity)
    {
        Physics2D.gravity = gravity * strength_joystick * GameData.spaceSize;
    }
}
