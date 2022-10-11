using UnityEngine;

public static class PositionExt
{
    // public static void SetPositionX(this Transform t, float x)
    // {
    //     Vector3 v = t.position;
    //     t.position = new Vector3(x, v.y, v.z);
    // }

    // public static void SetPositionY(this Transform t, float y)
    // {
    //     Vector3 v = t.position;
    //     t.position = new Vector3(v.x, y, v.z);
    // }

    // public static void SetPositionZ(this Transform t, float z)
    // {
    //     Vector3 v = t.position;
    //     t.position = new Vector3(v.x, v.y, z);
    // }

    public static void SetPositionXY(this Transform t, float x, float y)
    {
        Vector3 v = t.position;
        t.position = new Vector3(x, y, v.z);
    }



    public static void SetLocalPositionX(this Transform t, float x)
    {
        Vector3 v = t.localPosition;
        t.localPosition = new Vector3(x, v.y, v.z);
    }

    // public static void SetLocalPositionY(this Transform t, float y)
    // {
    //     Vector3 v = t.localPosition;
    //     t.localPosition = new Vector3(v.x, y, v.z);
    // }

    // public static void SetLocalPositionZ(this Transform t, float z)
    // {
    //     Vector3 v = t.localPosition;
    //     t.localPosition = new Vector3(v.x, v.y, z);
    // }
}