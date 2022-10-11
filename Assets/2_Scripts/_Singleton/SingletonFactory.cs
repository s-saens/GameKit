using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonFactory : MonoBehaviour
{
    private static bool singletonsWereMade = false;

    [SerializeField] private GameObject[] singletonObjects;

    private void Awake()
    {
        if(!singletonsWereMade)
        {
            foreach (GameObject go in singletonObjects)
            {
                DontDestroyOnLoad(Instantiate(go));
            }
            singletonsWereMade = true;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("DELETE PREFS");
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneController.Instance.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameData.wasPlaying.value = false;
            GameData.Last.floor.value = GameData.Last.floor.value + 1;
            GameData.highestFloor.value = Mathf.Max(GameData.Last.floor.value, GameData.highestFloor.value);
            SceneController.Instance.LoadScene(SceneEnum.Game);
        }

        Vector2 g = Vector2.zero;
        float s = 20;
        if (Input.GetKey(KeyCode.W)) g.y = s;
        if (Input.GetKey(KeyCode.S)) g.y = -s;
        if (Input.GetKey(KeyCode.D)) g.x = s;
        if (Input.GetKey(KeyCode.A)) g.x = -s;
        Physics2D.gravity = g;

    }
#endif
}
