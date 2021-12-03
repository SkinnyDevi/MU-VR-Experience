using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SubmitRatingHandler : MonoBehaviour
{
    static readonly string API_URL = "http://192.168.1.184:6996/ratings/rating/";

    public static IEnumerator SubmitRating(string objRatingName)
    {
        using(UnityWebRequest submitRating = createRatingUpdateRequest(API_URL, UserInfoManager.GetString("TempTKN"), ""))
        {
            yield return submitRating.SendWebRequest();
        }
    }

    private static UnityWebRequest createRatingUpdateRequest(string requestUrl, string bearerToken, string ratingJson)
	{	
		UnityWebRequest updateRequest = UnityWebRequest.Put(requestUrl, ratingJson);
		updateRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);
		updateRequest.SetRequestHeader("Content-Type", "application/json");

		return updateRequest;
	}
}
