using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using TMPro;

using SimpleJSON;

public class RatingPercentageHandler : MonoBehaviour
{
    static readonly string API_URL = "http://192.168.1.184:6996/ratings/by_type/";
    public TMP_Text LikedPercentage, RegularPercentage, DislikedPercentage;
    int clipId = -1;
    string token = "";

    public void SetRequestInfo(int id, string tkn)
    {
        clipId = id;
        token = tkn;
        StartCoroutine(CollectClipRatings());
    }

    IEnumerator CollectClipRatings()
    {
        using(UnityWebRequest ratings = createGetRequest(API_URL + clipId, token))
        {
            yield return ratings.SendWebRequest();

            if (ratings.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Couldn't complete ratings type request: " + ratings.error);
            }
            else
            {
                float liked, regulars, disliked, total = 0;
                JSONNode response = JSON.Parse(ratings.downloadHandler.text);
                liked = response["Liked"].Count;
                regulars = response["Regular"].Count;
                disliked = response["Disliked"].Count;
                total = liked + disliked + regulars;
                LikedPercentage.text = GetPercentage(liked, total);
                RegularPercentage.text = GetPercentage(regulars, total);
                DislikedPercentage.text = GetPercentage(disliked, total);
            }
        }
    }

    string GetPercentage(float x, float y)
    {
        return ((x/y)*100.0).ToString("0.0") + "%";
    }

    private UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}
}
