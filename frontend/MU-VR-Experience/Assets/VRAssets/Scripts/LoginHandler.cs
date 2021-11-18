using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using SimpleJSON;
using UserModel;

public class LoginHandler : MonoBehaviour
{
    static readonly string API_URL = "http://192.168.1.184:6996/users/signin";

    public static IEnumerator SignIn(string userEmail, string userPassword)
    {
        using(UnityWebRequest signin = createUserPostRequest(API_URL, userEmail, userPassword))
        {
            yield return signin.SendWebRequest(); 

            if (signin.result != UnityWebRequest.Result.Success)
            {
                if (signin.responseCode == 404)
                {
                    Debug.Log("User not found.");
                }
                else
                {
                    Debug.Log("There was an error signin in: " + signin.error);
                }
            }
            else
            {
                Debug.Log("Sign In Info --");
                JSONNode response = JSON.Parse(signin.downloadHandler.text);
                UserDataReceiver currentUserData  = GameObject.FindObjectOfType<UserDataReceiver>();
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
