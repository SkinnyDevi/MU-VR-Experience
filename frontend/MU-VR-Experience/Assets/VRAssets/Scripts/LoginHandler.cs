using UnityEngine;
using UnityEngine.Networking;

using System.Collections;
using System.Text.RegularExpressions;

using TMPro;

using SimpleJSON;
using UserModel;

public class LoginHandler : MonoBehaviour
{
    public GameObject SuccessObjText, ErrorObjText;
    public TMP_Text ErrorObjMessage;
	public static bool ValidationRetry = true;

    const string API_URL = "http://localhost:6996/users/signin";
    static GameObject s_successText, s_errorText;
    static TMP_Text s_errorMessage;

    void Start()
    {
        s_successText = SuccessObjText;
        s_errorText = ErrorObjText;
        s_errorMessage = ErrorObjMessage;
    }

    public static IEnumerator SignIn(string userEmail, string userPassword)
    {
		if (HandleErrorMessages(Validate(userEmail, userPassword)))
		{
			using(UnityWebRequest signin = CreateUserPostRequest(API_URL, userEmail, userPassword))
			{
				yield return signin.SendWebRequest(); 

				LoginKeyboardManager.ToggleProcessWheel(false);
				if (signin.result != UnityWebRequest.Result.Success)
				{
					s_errorMessage.text = "Password is Incorrect!";
					switch (signin.responseCode)
					{
						case 404:
							// Debug.Log("User not found!");
							s_errorMessage.text = "User not found!";
							break;
						case 401:
							s_errorMessage.text = "Password is Incorrect!";
							break;
						case 0:
							s_errorMessage.text = "No started\nserver was\nfound";
							break;
						default:
							// Debug.Log(signin.responseCode);
							s_errorMessage.text = "Request\nfound\nan error";
							Debug.LogError("There was an error signin in: " + signin.error);
							break;
					}
					s_errorText.SetActive(true);
				}
				else
				{
					s_errorText.SetActive(false);
					// Debug.Log("User signed in");
					
					JSONNode response = JSON.Parse(signin.downloadHandler.text);
					UserDataReceiver currentUserData  = GameObject.FindObjectOfType<UserDataReceiver>();
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

	private static int Validate(string email, string pass)
	{	
		int code = 1;
		Regex emailRx = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

		if (!(emailRx.Matches(email).Count > 0)) code = 0;
		
		if (string.IsNullOrEmpty(pass)) code = -1;
		if (string.IsNullOrEmpty(email)) code = -1;

		return code;
	}

	private static bool HandleErrorMessages(int validationCode)
	{
		switch (validationCode)
		{
			case -1:
				LoginKeyboardManager.ToggleProcessWheel(false);
				s_errorText.SetActive(true);
				s_errorMessage.text = "Missing required fields";
				return false;				
			case 0:
				LoginKeyboardManager.ToggleProcessWheel(false);
				s_errorText.SetActive(true);
				s_errorMessage.text = "Invalid email";
				return false;
			default:
				s_errorText.SetActive(false);
				ValidationRetry = false;
				return true;
		}
	}

    private static UnityWebRequest CreateUserPostRequest(string requestUrl, string requestEmail, string requestPassword)
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
