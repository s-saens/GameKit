using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public const float alpha = 0.9f;

    private RawImage image;

    private void Awake()
    {
        image = this.GetComponent<RawImage>();
        gameObject.SetActive(false);
    }
    
    public void Set(float to = alpha)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);
        image.raycastTarget = true;

        StartCoroutine(
            ((Vector4)image.color).To_Lerp(
                new Vector4(0, 0, 0, to),
                0.1f,
                (v) => image.color = v,
                true
            )
        );
    }
    public void Off()
    {
        StopAllCoroutines();

        image.raycastTarget = false;

        StartCoroutine(
            ((Vector4)image.color).To_Lerp(
                new Vector4(0, 0, 0, 0f),
                0.1f,
                (v) => image.color = v,
                true
            )
            .Then(()=>gameObject.SetActive(false))
        );
    }
}