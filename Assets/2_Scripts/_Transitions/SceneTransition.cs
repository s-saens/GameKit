using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public int sceneIndex;

    public virtual IEnumerator GetCoroutine()
    {
        yield return 0;
    }
}