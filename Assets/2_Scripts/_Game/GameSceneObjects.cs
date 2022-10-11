
using UnityEngine;

public class GameSceneObjects : SingletonMono<GameSceneObjects>
{
    public GameObject ball;
    public GameObject endPoint;
    public Camera cam;

    // Properties
    public Rigidbody2D ballRigid {
        get {
            return ball.GetComponent<Rigidbody2D>();
        }
    }
}