using UnityEngine;
using System.Collections;

public class CompleteController : MonoBehaviour
{
    [SerializeField] private Event completeEvent_;

    private void OnEnable()
    {
        completeEvent_.callback += CompleteAnimation;
    }
    private void OnDisable()
    {
        completeEvent_.callback -= CompleteAnimation;
    }

    [SerializeField] private EventVector3 moveCamEvent;
    [SerializeField] private EventFloat camOrthSizeEvent;
    [SerializeField] private AudioSource completeSound;
    [SerializeField] private AudioClip completeSE;
    [SerializeField] private float seVolume = 0.7f;

    private void CompleteAnimation()
    {
        GameData.isInvincible = true;
        GameData.isBallSinking = true;

        float velocity = GameSceneObjects.Instance.ballRigid.velocity.magnitude;
        completeSound.volume = Mathf.Clamp(Mathf.Log10(velocity) * 1.3f, 0, 1);
        completeSound.pitch = 0.5f + velocity / (GameData.spaceSize * 5);
        completeSound.Play();
        
        Vector3 endPosition = GameSceneObjects.Instance.endPoint.transform.position;
        moveCamEvent.Invoke(endPosition);
        StartCoroutine(BallTo(endPosition));
        StartCoroutine(
            BallSizeTo(endPosition)
            .Then(() => CamOrthsizeTo(0.0001f))
            .Then(Tween.Wait(0.5f))
            .Then(() => UpdateGameData())
            .Then(() => LoadScene())
        );
    }

    private void CamOrthsizeTo(float to)
    {
        SEController.Instance.Play(completeSE, seVolume);
        GameSceneObjects.Instance.ball.transform.localScale = Vector3.zero;
        camOrthSizeEvent.Invoke(0.0001f);
    }

    private IEnumerator BallTo(Vector3 to)
    {
        GameObject ball = GameSceneObjects.Instance.ball;
        yield return ball.transform.position.To_Lerp(to, 0.1f, (v) => ball.transform.position = v);
    }

    private IEnumerator BallSizeTo(Vector3 to)
    {
        Rigidbody2D ballRigid = GameSceneObjects.Instance.ballRigid;
        ballRigid.drag = 15;
        ballRigid.gravityScale = 0;
        ballRigid.velocity = Vector3.zero;

        Vector3 direction = to - ballRigid.transform.position;
        direction = direction.normalized;

        yield return ballRigid.transform.localScale.To_Lerp(Vector3.zero, 0.12f, (v) => {
            ballRigid.transform.localScale = v;
        });

        yield return 0;
    }

    private void UpdateGameData()
    {
        GameData.wasPlaying.value = false;
        GameData.Last.floor.value = GameData.Last.floor.value + 1;
        GameData.highestFloor.value = Mathf.Max(GameData.Last.floor.value, GameData.highestFloor.value);
    }

    private void LoadScene()
    {
        SceneController.Instance.LoadScene(SceneEnum.Game);
    }
}