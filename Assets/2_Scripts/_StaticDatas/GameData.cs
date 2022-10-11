using UnityEngine;
using System.Collections.Generic;

public static class GameData
{
    public static PrefsData<bool> wasPlaying = new PrefsData<bool>(KeyData.FLAG_WAS_PLAYING, false);
    public static PrefsData<bool> isNewbie = new PrefsData<bool>(KeyData.FLAG_IS_NEWBIE, true);
    public static PrefsData<int> highestFloor = new PrefsData<int>(KeyData.HIGHEST_FLOOR, 1);
    public static int savedFloor {
        get {
            return 5 * ((highestFloor.value - 1) / 5) + 1;
        }
    }
    public static PrefsData<float> camOrthSize = new PrefsData<float>(KeyData.CAM_ORTH_SIZE, 10);
    public static PrefsData<int> remainReviveChance = new PrefsData<int>(KeyData.REMAIN_RESURRECTION_CHANCE, 1);

    public static Vector2Int mazeSize {
        get {
            int f = Last.floor.value;
            f = (f-1) % 5 + 1 + (f-1)/5;
            if(f <= 0) return new Vector2Int(3, 3); 
            int s = f + 4;
            return new Vector2Int(s/2 + s%2, (s+1)/2 + (s+1)%2);
        }
    }

    public const float camOrthMin = 10;
    public static float camOrthMax
    {
        get
        {
            return camOrthMin + GameData.Last.floor.value * 0.5f;
        }
    }

    public static float spaceSize = 1;

    public static bool isInvincible = false;
    public static bool isBallSinking = false;
    public static Event backEvent = new Event();
    public static Event bgClickEvent = new Event();

    public static class Last
    {
        public static PrefsData<Maze> maze = new PrefsData<Maze>(KeyData.LAST_MAZE);
        public static PrefsData<Vector3> position = new PrefsData<Vector3>(KeyData.LAST_POSITION);
        public static PrefsData<int> floor = new PrefsData<int>(KeyData.LAST_FLOOR, 1);
    }

    public static class Settings
    {
        public static PrefsData<bool> joystickPosition = new PrefsData<bool>(KeyData.SETTINGS_JOYSTICK_POSITION, false); // false : left
        public static PrefsData<bool> cameraFollowing = new PrefsData<bool>(KeyData.SETTINGS_CAMERA_FOLLOWING, true);
        public static Dictionary<string, PrefsData<float>> volumes = new Dictionary<string, PrefsData<float>>() {
            {KeyData.SETTINGS_VOLUME_MASTER, new PrefsData<float>(KeyData.SETTINGS_VOLUME_MASTER, 1)},
            {KeyData.SETTINGS_VOLUME_ME, new PrefsData<float>(KeyData.SETTINGS_VOLUME_ME, 1)},
            {KeyData.SETTINGS_VOLUME_SE, new PrefsData<float>(KeyData.SETTINGS_VOLUME_SE, 1)},
            {KeyData.SETTINGS_VOLUME_BGM, new PrefsData<float>(KeyData.SETTINGS_VOLUME_BGM, 1)},
        };
    }

    public static class Temporary
    {
        public static Maze lastMaze;
        public static Vector3 lastPosition;
        public static int lastFloor;
    }

    public static class IAP
    {
        public static Dictionary<string, PrefsData<bool>> non_consumable = new Dictionary<string, PrefsData<bool>>() {
            {KeyData.IAP_remove_ads_revive, new PrefsData<bool>(KeyData.IAP_remove_ads_revive, false)},
            {KeyData.IAP_remove_ads_restart, new PrefsData<bool>(KeyData.IAP_remove_ads_restart, false)},
            {KeyData.IAP_revive_chance, new PrefsData<bool>(KeyData.IAP_revive_chance, false)}
        };
        public static Dictionary<string, PrefsData<int>> consumable = new Dictionary<string, PrefsData<int>>() {
        };
    }

    public static Data<bool> isAdInitialized = new Data<bool>(false);
    public static Data<bool> isAdLoaded = new Data<bool>(false);
}