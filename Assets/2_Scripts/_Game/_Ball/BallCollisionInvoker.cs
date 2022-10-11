using UnityEngine;

public class BallCollisionInvoker : MonoBehaviour
{
    [SerializeField] private EventFloat ballCollisionEvent; // 부딪히는 ME
    [SerializeField] private EventFloat ballMoveEvent; // 구르는 ME
    [SerializeField] private Event deathEvent;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            float strength = Vector2.Dot(GetNormal(collision.contacts), rigid.velocity);
            ballCollisionEvent.Invoke(strength);
        }
    }

    // Obstacle
    private void OnTriggerStay2D(Collider2D other)
    {
        if(!GameData.isInvincible && other.tag == "Obstacle") deathEvent.Invoke();
    }
    
    private void Update()
    {
        ballMoveEvent.Invoke(rigid.velocity.magnitude);
    }

    private Vector2 GetNormal(ContactPoint2D[] contacts)
    {
        if(contacts.Length == 0) return Vector2.zero;
        
        Vector2 sum = Vector2.zero;
        foreach(var c in contacts) sum += c.normal;

        return sum / contacts.Length;
    }
}