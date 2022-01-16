using UnityEngine;
using UnityEngine.Networking;

using WebSocketSharp;

using System.Collections;

using RatingModel;
using SimpleJSON;

public class SubmitRatingHandler : MonoBehaviour
{
	public static int UserID = -1;

	const string API_URL = "http://localhost:6996/ratings/";
	const string WS_URL = "ws://localhost:6996/ratings";
	static string s_requestToken;
	static UserDataReceiver s_playerData;
	static Rating s_ratingPayload;
	static WebSocket _ws;

	void Start()
	{
		s_playerData = GameObject.FindObjectOfType<UserDataReceiver>();
		s_playerData.SetToken(UserInfoManager.GetString("TempTKN"));
		s_requestToken = s_playerData.GetToken();

		_ws = new WebSocket(WS_URL);
        _ws.Connect();
        _ws.OnMessage += (sender, e) =>
        {
            Debug.Log("SRH - "+((WebSocket)sender).Url+", Data : "+e.Data);
        };
	}

	public static IEnumerator SubmitRating(string objName, GameObject Wall, GameObject Doors)
	{
		s_ratingPayload = GenerateNewRating();

		using (UnityWebRequest checkExistence = CreateGetRequest(API_URL + $"submitted_rating/exists/{s_ratingPayload.GetUserID()}/{s_ratingPayload.GetClipID()}", s_requestToken))
		{
			yield return checkExistence.SendWebRequest();

			if (checkExistence.result != UnityWebRequest.Result.Success)
			{
				if (checkExistence.responseCode == 404)
				{
					s_ratingPayload.SetRatingID(0);
				}
				else
				{
					Debug.LogError("Couldn't Complete Existance Request: " + checkExistence.error);
				}
			}
			else
			{
				JSONNode response = JSON.Parse(checkExistence.downloadHandler.text);
				s_ratingPayload.SetRatingID(response["rating_id"]);
			}
			s_ratingPayload.SetRating(objName.Replace("Cube", ""));
		}

		using (UnityWebRequest submitRating = CreateRatingUpdateRequest(API_URL + "rating/", s_requestToken, s_ratingPayload))
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

				string ws_event = "{\"event\": \"newRating\",\n\"payload\": {\n\"clipId\": \"" + s_ratingPayload.GetUserID() + "\"\n}}";
				_ws.Send(ws_event);
			}
		}
	}

	private static Rating GenerateNewRating()
	{
		Rating temp = new Rating();
		temp.SetUserID(UserID);
		temp.SetClipID(UserInfoManager.GetInt("VideoID"));
		return temp;
	}

	private static UnityWebRequest CreateGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken);

		return getUsers;
	}

	private static UnityWebRequest CreateRatingUpdateRequest(string requestUrl, string bearerToken, Rating userRating)
	{
		UnityWebRequest updateRequest = UnityWebRequest.Put(requestUrl, userRating.RatingToJson());
		updateRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);
		updateRequest.SetRequestHeader("Content-Type", "application/json");

		return updateRequest;
	}
}
