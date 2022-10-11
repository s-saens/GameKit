using UnityEngine;
using System.Collections.Generic;

public class RankingView : MonoBehaviour
{
    private RankingInfoContent[] contents;

    private void OnEnable()
    {
        contents = GetComponentsInChildren<RankingInfoContent>();
        SetContents();
    }

    private void SetContents()
    {
        foreach(var c in contents)
        {
            // c.Set(url, userName, rank);
        }
    }
}