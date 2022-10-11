using UnityEngine;

public class BallPrefsController : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GameData.Last.position.value = this.transform.position;
    }
}