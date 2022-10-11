using UnityEngine;
using Newtonsoft.Json;

public class GravityInput : MonoBehaviour
{
    public EventVector2 gravityEvent;

    private Gyroscope gyro;
    private bool gyroEnabled;

    private void Start()
    {
        gyroEnabled = SystemInfo.supportsGyroscope;
        if (gyroEnabled == false)
        {
            return;
        }

        gyro = Input.gyro;
        gyro.enabled = true;
    }

    private void Update()
    {
        if(gyroEnabled == false) return;
        gravityEvent.Invoke(gyro.gravity);
    }
}