using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollwer : MonoBehaviour
{
    public GameSceneObjects obj;
    private void Update()
    {
        this.transform.position = obj.ball.transform.position;
    }
}
