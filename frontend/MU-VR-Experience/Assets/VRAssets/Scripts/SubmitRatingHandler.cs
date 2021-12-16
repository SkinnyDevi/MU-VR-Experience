using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using RatingModel;
using SimpleJSON;

public class SubmitRatingHandler : MonoBehaviour
{
	public static int UserID = -1;

	static readonly string API_URL = "http://localhost:6996/ratings/";
	static string TEMP_TKN;
	static UserDataReceiver playerData;
	static Rating ratingPayload;

	void Start()
	{
		playerData = GameObject.FindObjectOfType<UserDataReceiver>();
		playerData.SetToken(UserInfoManager.GetString("TempTKN"));
		TEMP_TKN = playerData.GetToken();
	}

	public static IEnumerator RatingSubmitTest(string objName, GameObject Wall, GameObject Doors)
	{
		ratingPayload = GenerateNewRating();

		using (UnityWebRequest checkExistence = createGetRequest(API_URL + $"submitted_rating/exists/{ratingPayload.GetUserID()}/{ratingPayload.GetClipID()}", TEMP_TKN))
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
		}

		using (UnityWebRequest submitRating = createRatingUpdateRequest(API_URL + "rating/", TEMP_TKN, ratingPayload))
		{
			yield return submitRating.SendWebRequest();

			if (submitRating.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Couldn't Complete Request: " + submitRating.downloadHandler.text);
			}
			else
			{
				Wall.SetActive(false);
				Doors.SetActive(true);
			}
		}
	}

	static Rating GenerateNewRating()
	{
		Rating temp = new Rating();
		temp.SetUserID(UserID);
		temp.SetClipID(UserInfoManager.GetInt("VideoID"));
		return temp;
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
