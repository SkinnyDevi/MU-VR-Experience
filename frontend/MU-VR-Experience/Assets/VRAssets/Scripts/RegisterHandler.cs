using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using SimpleJSON;
using UserModel;

public class RegisterHandler : MonoBehaviour
{
    static readonly string API_URL = "http://192.168.1.184:6996/users/";

	public static IEnumerator CreateUserFromPlayer(string userEmail, string userPwd)
	{
		using(UnityWebRequest createUser = createUserPostRequest(API_URL, userEmail, userPwd))
		{
			yield return createUser.SendWebRequest();

			if (createUser.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(createUser.responseCode);
				Debug.LogError("Something went wrong: " + createUser.error);
			}
			else
			{
				Debug.Log("New user info: ");
				JSONNode response = JSON.Parse(createUser.downloadHandler.text);
				UserDataReceiver currentUserData = GameObject.FindObjectOfType<UserDataReceiver>();
				JSONNode userObj = response["user"];
				User newPlayer = new User(userObj["user_id"], userObj["password"], userObj["email"], userObj["username"], bool.Parse(userObj["isAdmin"]));
				currentUserData.SetCurrentPlayer(newPlayer);
				currentUserData.SetToken(response["access_token"]);
			}
		}
	}

	private static UnityWebRequest createUserPostRequest(string requestUrl, string requestEmail, string requestPassword)
	{	
		UnityWebRequest postRequest = UnityWebRequest.Post(requestUrl, Authentication(requestEmail, requestPassword));
		postRequest.SetRequestHeader("Authorization", Authentication(requestEmail, requestPassword));

		return postRequest;
	}

	private static string Authentication(string email, string password)
	{
		string basicAuthUser = email + ":" + password;
		basicAuthUser = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(basicAuthUser));
		basicAuthUser = "Basic " + basicAuthUser;
		return basicAuthUser;
	}
}
