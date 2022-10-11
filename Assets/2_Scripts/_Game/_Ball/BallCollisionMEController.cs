using UnityEngine;

public class BallCollisionMEController : MonoBehaviour
{
    public AudioSource rollSound;
    public AudioSource collideSound;
    public EventFloat ballCollisionEvent_;
    public EventFloat ballMoveEvent_;

    public void OnEnable()
    {
        ballCollisionEvent_.callback += OnCollide;
        ballMoveEvent_.callback += OnRoll;
    }

    public void OnDisable()
    {
        ballCollisionEvent_.callback -= OnCollide;
        ballMoveEvent_.callback -= OnRoll;
    }

    private void OnCollide(float normalVelocity) // float
    {
        collideSound.volume = Mathf.Clamp(normalVelocity * 0.6f, 0, 1);
        collideSound.pitch = 1f + normalVelocity * 0.05f;
        collideSound.Play();
    }

    private void OnRoll(float velocity) // float
    {
        rollSound.volume = Mathf.Clamp(Mathf.Log10(velocity) * 1.8f, 0, 1);
        rollSound.pitch = 0.5f + velocity / (GameData.spaceSize * 5);
    }
}