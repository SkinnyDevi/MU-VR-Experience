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

    static readonly string API_URL = "http://192.168.1.184:6996/users/signin";
    static GameObject SuccessText, ErrorText;
    static TMP_Text ErrorMessage;

    void Start()
    {
        SuccessText = SuccessObjText;
        ErrorText = ErrorObjText;
        ErrorMessage = ErrorObjMessage;
    }

    public static IEnumerator SignIn(string userEmail, string userPassword)
    {
		if (HandleErrorMessages(Validate(userEmail, userPassword)))
		{
			using(UnityWebRequest signin = createUserPostRequest(API_URL, userEmail, userPassword))
			{
				yield return signin.SendWebRequest(); 

				if (signin.result != UnityWebRequest.Result.Success)
				{
					ErrorMessage.text = "Password is Incorrect!";
					switch (signin.responseCode)
					{
						case 404:
							Debug.Log("User not found!");
							ErrorMessage.text = "User not found!";
							break;
						case 401:
							ErrorMessage.text = "Password is Incorrect!";
							break;
						default:
							ErrorMessage.text = "Request found an error";
							Debug.LogError("There was an error signin in: " + signin.error);
							break;
					}
					ErrorText.SetActive(true);
				}
				else
				{
					ErrorText.SetActive(false);
					Debug.Log("User signed in");
					
					JSONNode response = JSON.Parse(signin.downloadHandler.text);
					UserDataReceiver currentUserData  = GameObject.FindObjectOfType<UserDataReceiver>();
					JSONNode userObj = response["user"];
					User newPlayer = new User(userObj["user_id"], userObj["email"], userObj["username"]);

					SuccessText.SetActive(true);

					currentUserData.SetCurrentPlayer(newPlayer);
					currentUserData.SetToken(response["access_token"]);
				}
			}
        }
		ValidationRetry = true;
    }

	static int Validate(string email, string pass)
	{	
		int code = 1;
		Regex emailRx = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

		Debug.Log("INFO: " + email + ", " + pass);
		if (string.IsNullOrEmpty(email)) code = -1;
		else if (!(emailRx.Matches(email).Count > 0)) code = 0;

		if (string.IsNullOrEmpty(pass)) code = -1;

		return code;
	}

	static bool HandleErrorMessages(int validationCode)
	{
		switch (validationCode)
		{
			case -1:
				ErrorText.SetActive(true);
				ErrorMessage.text = "Missing required fields";
				return false;				
			case 0:
				ErrorText.SetActive(true);
				ErrorMessage.text = "Invalid email";
				return false;
			default:
				ErrorText.SetActive(false);
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
