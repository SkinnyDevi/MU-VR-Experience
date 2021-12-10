using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using SimpleJSON;
using UserModel;

public class UserDataReceiver : MonoBehaviour
{
	public Button LoginExitButton;
	public Button RegisterExitButton;
	public GameObject Crosshair;
	public TMP_Text SettingsUserText;

	static readonly string userUrl = "http://192.168.1.184:6996/users/user/";

	string token = "";
	User player = new User();

	void Start()
	{
		player = new User();
		UserInfoManager.LoadSettings();
		if (!SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
		{
			StartCoroutine(LoadNewPlayer());
		}
	}

	void OnApplicationQuit() // Only for standalone
	{
		UserInfoManager.DeleteInfoOnExit();
	}

	public User CurrentPlayer()
	{
		return this.player;
	}

	public void SetCurrentPlayer(User newPlayer)
	{
		this.player = newPlayer;

		if (SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
		{
			GameObject.FindObjectOfType<UserTransition>().TriggerSuccessExit();
			bool currentObject = Crosshair.GetComponent<PointerControls>().GetCurrentObject().Equals("Login");
			if (currentObject) LoginExitButton.onClick.Invoke();
			else RegisterExitButton.onClick.Invoke();
		}

		SettingsUserText.text = "User: " + this.player.GetUsername();
		SettingsUserText.gameObject.SetActive(true);
	}

	IEnumerator LoadNewPlayer()
	{
		using(UnityWebRequest getUser = createGetRequest(userUrl + UserInfoManager.GetInt("User"), UserInfoManager.GetString("TempTKN")))
		{
			yield return getUser.SendWebRequest();

			if (getUser.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Something went wrong: " + getUser.error);
			}
			else
			{
				JSONNode response = JSON.Parse(getUser.downloadHandler.text);

				SetCurrentPlayer(new User(response["user_id"], response["email"], response["username"]));
				SetToken(UserInfoManager.GetString("TempTKN"));

				if (SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.TheatreBillboard.ToString()))
					GameObject.FindObjectOfType<ImageFramesSpawner>().LoadImageFrames();
			}
		}
	}
	
	public void SetToken(string tkn)
	{
		token = tkn;
		if (SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
			GameObject.Find("Environment/RegisterRoom/Walls/BillBoardEntry").SetActive(true);
		UserInfoManager.SaveUser(this.player.GetId(), tkn);
	}

	public string GetToken()
	{
		return token;
	}

	private UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}

	/*
	private UnityWebRequest createUserPostRequest(string requestUrl, string requestEmail, string requestPassword)
	{	
		UnityWebRequest postRequest = UnityWebRequest.Post(requestUrl, Authentication(requestEmail, requestPassword));
		postRequest.SetRequestHeader("Authorization", Authentication(requestEmail, requestPassword));

		return postRequest;
	}

	private UnityWebRequest createUserDeleteRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest deleteRequest = UnityWebRequest.Delete(requestUrl);
		deleteRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);

		return deleteRequest;
	}

	private UnityWebRequest createUserUpdateRequest(string requestUrl, string bearerToken, User updateUser)
	{	
		UnityWebRequest updateRequest = UnityWebRequest.Put(requestUrl, updateUser.UserToJson());
		updateRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);
		updateRequest.SetRequestHeader("Content-Type", "application/json");

		return updateRequest;
	}

	private string Authentication(string email, string password)
	{
		string basicAuthUser = email + ":" + password;
		basicAuthUser = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(basicAuthUser));
		basicAuthUser = "Basic " + basicAuthUser;
		return basicAuthUser;
	}
	*/
}
