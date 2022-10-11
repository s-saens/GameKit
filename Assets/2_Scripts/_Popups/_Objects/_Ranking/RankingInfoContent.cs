using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RankingInfoContent : MonoBehaviour
{
    [SerializeField] private RawImage photo;
    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text rank;

    public void Set(string photoUrl, string _userName, string _rank)
    {
        SetPhoto(photoUrl);
        userName.text = _userName;
        rank.text = _rank;       
    }

    private void SetPhoto(string photoUrl)
    {
        StopAllCoroutines();
        StartCoroutine(SetPhotoCoroutine(photoUrl));
    }


    private IEnumerator SetPhotoCoroutine(string photoUrl)
    {
        using (UnityWebRequest webReq = UnityWebRequestTexture.GetTexture(photoUrl))
        {
            yield return webReq.SendWebRequest();

            if(webReq.result != UnityWebRequest.Result.Success) Debug.LogError(webReq.error);
            else photo.texture = (Texture)DownloadHandlerTexture.GetContent(webReq);
        }
    }
}