using UnityEngine;
using Newtonsoft.Json;

public static class JsonSettings
{
    private static JsonSerializerSettings s = new JsonSerializerSettings();
    public static JsonSerializerSettings Settings
    {
        get {
            s.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return s;
        }
    }
}