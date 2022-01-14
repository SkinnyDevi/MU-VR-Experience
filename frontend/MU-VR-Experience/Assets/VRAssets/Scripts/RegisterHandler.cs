using UnityEngine;
using UnityEngine.Networking;

using System.Collections;
using System.Text.RegularExpressions;

using TMPro;

using SimpleJSON;
using UserModel;

public class RegisterHandler : MonoBehaviour
{
	public GameObject SuccessObjText, ErrorObjText;
	public TMP_Text ErrorObjMessage;
	public static bool ValidationRetry = true;

    const string API_URL = "http://localhost:6996/users/";
	static GameObject s_successText, s_errorText;
	static TMP_Text s_errorMessage;

	void Start()
    {
        s_successText = SuccessObjText;
        s_errorText = ErrorObjText;
        s_errorMessage = ErrorObjMessage;
    }

	public static IEnumerator CreateUserFromPlayer(string userEmail, string userPwd, string userConfirmPwd)
	{
		if (HandleErrorMessages(Validate(userEmail, userPwd, userConfirmPwd)))
		{
			using(UnityWebRequest createUser = createUserPostRequest(API_URL, userEmail, userPwd))
			{
				yield return createUser.SendWebRequest();

				RegisterKeyboardManager.ToggleProcessWheel(false);
				if (createUser.result != UnityWebRequest.Result.Success)
				{
					switch (createUser.responseCode)
					{
						case 0:
							s_errorMessage.text = "No started\nserver was\nfound";
							break;
						default:
							// Debug.Log(createUser.responseCode);
							s_errorMessage.text = "Request\nfound\nan error";
							Debug.LogError("There was an error registering the user in: " + createUser.error);
							break;
					}
					s_errorText.SetActive(true);
				}
				else
				{
					s_errorText.SetActive(false);
					// Debug.Log("New user submitted");

					JSONNode response = JSON.Parse(createUser.downloadHandler.text);
					UserDataReceiver currentUserData = GameObject.FindObjectOfType<UserDataReceiver>();
					JSONNode userObj = response["user"];
					User newPlayer = new User(userObj["user_id"], userObj["email"], userObj["username"]);
					
					s_successText.SetActive(true);
					
					currentUserData.SetCurrentPlayer(newPlayer);
					currentUserData.SetToken(response["access_token"]);
				}
			}
		}
		ValidationRetry = true;
	}

	private static int Validate(string email, string pass, string confirmPass)
	{
		int code = 1;

		Regex emailRx = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

		if (!pass.Equals(confirmPass)) code = -2;

		if (!(emailRx.Matches(email).Count > 0)) code = 0;
		if (string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPass)) code = -1;
		if (string.IsNullOrEmpty(email)) code = -1;

		return code;
	}

	private static bool HandleErrorMessages(int validationCode)
	{
		switch (validationCode)
		{
			case -1:
				RegisterKeyboardManager.ToggleProcessWheel(false);
				s_errorText.SetActive(true);
				s_errorMessage.text = "Missing required fields";
				return false;				
			case 0:
				RegisterKeyboardManager.ToggleProcessWheel(false);
				s_errorText.SetActive(true);
				s_errorMessage.text = "Invalid email";
				return false;
			case -2:
				RegisterKeyboardManager.ToggleProcessWheel(false);
				s_errorText.SetActive(true);
				s_errorMessage.text = "Passwords don't match!";
				return false;
			default:
				s_errorText.SetActive(false);
				ValidationRetry = false;
				return true;
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
