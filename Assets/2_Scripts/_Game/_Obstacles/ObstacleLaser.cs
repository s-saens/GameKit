using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleLaser : MonoBehaviour
{
    [SerializeField] private bool randomizeTime = false;
    [SerializeField] private float aimTime = 2;
    [SerializeField] private float aimmingWidth = 0.05f;
    [SerializeField] private float aimmingPercentage = 0.2f;
    [SerializeField] private float stopTime = 0.5f;
    [SerializeField] private float emissionInitWidth = 0.4f;
    [SerializeField] private float emitPercentage = 0.2f;
    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private int penetration = 0;
    [SerializeField] private Color aimColor;
    [SerializeField] private Color emissionColor;


    [SerializeField] private AudioClip laserSE;

    private bool aimming = false;
    private RaycastHit2D[] rayHits;
    [SerializeField] private LineRenderer line;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private void Start()
    {
        LookBall(1);
        SetWidth(0);
        StartCoroutine(LaserProcess());
    }


    private void Update()
    {
        if(GameData.isBallSinking)
        {
            SetWidth(0);
            StopAllCoroutines();
            return;
        }

        LookBall(0.2f);
        RenderRay();
    }

    private void LookBall(float percentage)
    {
        if(!aimming) return;

        Vector2 rayDirection = (GameSceneObjects.Instance.ball.transform.position - this.transform.position).normalized;

        float rotationZ = Mathf.Acos(rayDirection.x / rayDirection.magnitude) * 180 / Mathf.PI * Mathf.Sign(rayDirection.y);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, rotationZ), percentage);
    }

    private void RenderRay()
    {
        Vector3 originPoint = this.transform.position; // + this.transform.right * this.transform.localScale.x * 0.5f;

        // Cast Ray
        rayHits = Physics2D.RaycastAll(originPoint, this.transform.right);
        if(rayHits.Length <= 0) return;

        // Find the First Wall except ones are near the head
        Vector3 zAdjuster = Vector3.forward * this.transform.position.z;

        // Set Points
        line.SetPosition(0, originPoint);
        line.SetPosition(1, (Vector3)rayHits[FindHitPoint(originPoint)].point + zAdjuster);
    }

    private int FindHitPoint(Vector2 originPoint)
    {
        int p = penetration;
        int ret = 0;
        for(int i=0 ; i<rayHits.Length ; ++i)
        {
            if(Vector2.Distance(originPoint, rayHits[i].point) > GameData.spaceSize * 0.2f)
            {
                ret = i;
                if(p == 0) break;
                p--;
            }
        }

        return ret;
    }

    private float randomizer
    {
        get {
            float adjuster = 0.1f;
            return randomizeTime ? Random.Range(-startDelay * adjuster, startDelay * adjuster) : 0;
        }
    }
    private IEnumerator LaserProcess()
    {
        edgeCollider.enabled = false;
        yield return Tween.Wait(startDelay + randomizer);
        aimming = true;
        Aim();
        yield return Tween.Wait(aimTime);
        StopAim();
        aimming = false;
        yield return Tween.Wait(stopTime);
        yield return EmitLaser();
        yield return LaserProcess();
    }

    private void Aim()
    {
        line.material.SetColor("_CenterColor", aimColor);
        line.material.SetColor("_GlowColor", aimColor);
        StartCoroutine(0f.To_Lerp(aimmingWidth, aimmingPercentage, SetWidth, false));
    }
    private void StopAim()
    {
        line.gameObject.SetActive(false);
    }

    private IEnumerator EmitLaser()
    {
        SEController.Instance.Play(laserSE, 0.3f, 2);
        
        line.material.SetColor("_CenterColor", Color.white);
        line.material.SetColor("_GlowColor", emissionColor);
        line.gameObject.SetActive(true);
        edgeCollider.enabled = true;

        yield return emissionInitWidth.To_Lerp(0, emitPercentage, (v) => { SetWidth(v); SetCollider(v); });
        SetWidth(0);
        yield return 0;
    }

    private void SetCollider(float w)
    {
        edgeCollider.edgeRadius = w * 0.3f;

        List<Vector2> points = new List<Vector2>();
        Vector2 dist = line.GetPosition(0) - line.GetPosition(1);
        points.Add(Vector2.zero);
        points.Add(Vector2.right * dist.magnitude);
        edgeCollider.SetPoints(points);
    }

    private void SetWidth(float w)
    {
        if(w < 0.01f) edgeCollider.enabled = false;
        line.startWidth = w;
        line.endWidth = w;
    }
}