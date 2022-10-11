using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] private Event deathEvent_;
    [SerializeField] private float maxR = 4f;
    [SerializeField] private float minR = 1.5f;

    private ParticleSystem particle;

    private void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        deathEvent_.callback += OnDeath;
    }

    private void OnDisable()
    {
        deathEvent_.callback -= OnDeath;
    }
    
    private void OnDeath()
    {
        particle.Play();
        GameSceneObjects.Instance.ball.SetActive(false);
    }
}