using UnityEngine;
using System;
using System.Collections;

public static class Tween
{
    public static IEnumerator WaitAFrame()
    {
        yield return 0;
    }
    
    public static IEnumerator To_Lerp(this float value, float to, float percentage, Action<float> function, bool ignorePause = false)
    {
        while(MathF.Abs(value - to) > 0.01f)
        {
            float deltaTime = ignorePause ? 1 : Time.deltaTime * 60;
            value = Mathf.Lerp(value, to, percentage * deltaTime);
            function.Invoke(value);
            if(ignorePause) yield return 0;
            else yield return new WaitForFixedUpdate();
        }
        value = to;
        function.Invoke(value);
    }

    public static IEnumerator To_Linear(this float value, float to, float speed, Action<float> function, bool ignorePause = false)
    {
        while (MathF.Abs(value - to) > 0.01f)
        {
            float deltaTime = ignorePause ? 1 : Time.deltaTime * 60;
            value += speed * deltaTime * MathF.Sign(to - value);
            function.Invoke(value);
            if (ignorePause) yield return 0;
            else yield return new WaitForFixedUpdate();
        }
        value = to;
        function.Invoke(value);
    }

    public static IEnumerator To_Lerp(this Vector2 value, Vector2 to, float percentage, Action<Vector2> function, bool ignorePause = false)
    {
        while ((value - to).magnitude > 0.01f)
        {
            float deltaTime = ignorePause ? 1 : Time.deltaTime * 60;
            value = Vector2.Lerp(value, to, percentage * deltaTime);
            function.Invoke(value);
            if (ignorePause) yield return 0;
            else yield return new WaitForFixedUpdate();
        }
        value = to;
        function.Invoke(value);
    }

    public static IEnumerator To_Lerp(this Vector3 value, Vector3 to, float percentage, Action<Vector3> function, bool ignorePause = false)
    {
        while ((value - to).magnitude > 0.01f)
        {
            float deltaTime = ignorePause ? 1 : Time.deltaTime * 60;
            value = Vector3.Lerp(value, to, percentage * deltaTime);
            function.Invoke(value);
            if(ignorePause) yield return 0;
            else yield return new WaitForFixedUpdate();
        }
        value = to;
        function.Invoke(value);
    }

    public static IEnumerator To_Lerp(this Vector4 value, Vector4 to, float percentage, Action<Vector4> function, bool ignorePause = false)
    {
        while ((value - to).magnitude > 0.01f)
        {
            float deltaTime = ignorePause ? 1 : Time.deltaTime * 60;
            value = Vector4.Lerp(value, to, percentage * deltaTime);
            function.Invoke(value);
            if(ignorePause) yield return 0;
            else yield return new WaitForFixedUpdate();
        }
        value = to;
        function.Invoke(value);
    }

    public static IEnumerator Wait(float time, bool ignorePause = false)
    {
        if(ignorePause) yield return new WaitForSecondsRealtime(time);
        else yield return new WaitForSeconds(time);
        yield return 0;
    }
}