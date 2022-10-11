using UnityEngine;
using UnityEngine.UI;

public class InvincibilityController : MonoBehaviour
{
    [SerializeField] private EventFloat invincibilityEvent_;

    private void OnEnable()
    {
        invincibilityEvent_.callback += SetBallInvincible;
    }
    private void OnDisable()
    {
        invincibilityEvent_.callback -= SetBallInvincible;
    }


    [SerializeField] private float invincibilityAlpha = 0.3f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void SetBallInvincible(float time)
    {
        StopAllCoroutines();
        GameData.isInvincible = true;
        InvincibleEffectOn();
        StartCoroutine(Tween.Wait(time)
            .Then(()=>GameData.isInvincible = false)
            .Then(()=>InvincibleEffectOff())
        );
    }
    private void InvincibleEffectOn()
    {
        Color c = spriteRenderer.color;
        spriteRenderer.color = new Color(c.r, c.b, c.g, invincibilityAlpha);
    }

    private void InvincibleEffectOff()
    {
        Color c = spriteRenderer.color;
        float nowAlpha = c.a;
        StartCoroutine(nowAlpha.To_Lerp(1, 0.1f, (v) => spriteRenderer.color = new Color(c.r, c.b, c.g, v)));
    }
}