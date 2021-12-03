using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using RatingModel;
using SimpleJSON;

public class SubmitRatingHandler : MonoBehaviour
{
	public static bool hasCheckedExistence = false;
	static bool finishedUpdating = false;
	static readonly string API_URL = "http://192.168.1.184:6996/ratings/";
	static string TEMP_TKN;
	static Rating ratingPayload = new Rating();

	void Start()
	{
		TEMP_TKN = UserInfoManager.GetString("TempTKN");
	}

	void Update()
	{
		if (finishedUpdating)
		{
			StopAllCoroutines();
			finishedUpdating = false;
		}
	}

	public static IEnumerator CheckRatingExistence(string objName)
	{
		ratingPayload.SetUserID(UserInfoManager.GetInt("User"));
		ratingPayload.SetClipID(UserInfoManager.GetInt("VideoID"));
		using(UnityWebRequest checkExistence = createGetRequest(API_URL + $"submitted_rating/exists/{ratingPayload.GetUserID()}/{ratingPayload.GetClipID()}", TEMP_TKN))
		{
			yield return checkExistence.SendWebRequest();

			if (checkExistence.result != UnityWebRequest.Result.Success)
			{
				if (checkExistence.responseCode == 404)
				{
					ratingPayload.SetRatingID(0);
				}
				else
				{
					Debug.LogError("Couldn't Complete Existance Request: " + checkExistence.error);
				}
			}
			else
			{
				JSONNode response = JSON.Parse(checkExistence.downloadHandler.text);
				ratingPayload.SetRatingID(response["rating_id"]);
			}
			ratingPayload.SetRating(objName.Replace("Cube", ""));
			hasCheckedExistence = true;
		}
	}

	public static IEnumerator SubmitRating(GameObject Wall, GameObject Doors)
    {
		if (!finishedUpdating)
		{
			using(UnityWebRequest submitRating = createRatingUpdateRequest(API_URL + "rating/", TEMP_TKN, ratingPayload))
			{
				yield return submitRating.SendWebRequest();

				if (submitRating.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError("Couldn't Complete Request: " + submitRating.downloadHandler.text);
				}
				else
				{
					Debug.Log(submitRating.downloadHandler.text);
					Wall.SetActive(false);
					Doors.SetActive(true);
					GameObject.FindObjectOfType<PointerControls>().SetCurrentObject("RatingButtonExit");
					finishedUpdating = true;
				}
			}
		}
    }

	private static UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}

    private static UnityWebRequest createRatingUpdateRequest(string requestUrl, string bearerToken, Rating userRating)
	{	
		UnityWebRequest updateRequest = UnityWebRequest.Put(requestUrl, userRating.RatingToJson());
		updateRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);
		updateRequest.SetRequestHeader("Content-Type", "application/json");

		return updateRequest;
	}
}
