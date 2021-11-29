using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

	// static readonly string Url = "http://192.168.1.184:6996/users/";

	string token = "";
	User player = new User();

	void Start()
	{
		player = new User();
		UserInfoManager.LoadSettings();
		//StartCoroutine(CreateUser(url, this.player));
		//StartCoroutine(GetUsers(url, token));
		//StartCoroutine(GetUsers(url, token, true, 5));
		//StartCoroutine(UpdateUser(url, token, this.player));
		//StartCoroutine(DeleteUser(url, token, this.player));
	}

	public User CurrentPlayer()
	{
		return this.player;
	}

	public void SetCurrentPlayer(User newPlayer)
	{
		this.player = newPlayer;

		GameObject.FindObjectOfType<UserTransition>().TriggerSuccessExit();

		bool currentObject = Crosshair.GetComponent<PointerControls>().GetCurrentObject().Equals("Login");
		if (currentObject) LoginExitButton.onClick.Invoke();
		else RegisterExitButton.onClick.Invoke();

		SettingsUserText.text = "User: " + this.player.GetUsername();
		SettingsUserText.gameObject.SetActive(true);
	}
	
	public void SetToken(string tkn)
	{
		token = tkn;
		GameObject.Find("Environment/RegisterRoom/Walls/BillBoardEntry").SetActive(true);
		Debug.Log(token);
	}

	public string GetToken()
	{
		return token;
	}

	/*
	// Get Users With or Without ID
	public IEnumerator GetUsers(string getUrl, string userToken, bool withID = false, int userID = 0)
	{
		if (withID) getUrl += "user/" + userID;
		using(UnityWebRequest getter = createGetRequest(getUrl, userToken))
		{	
			yield return getter.SendWebRequest();
			if (getter.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Something went wrong: " + getter.error);
			}
			else
			{
				JSONNode response = JSON.Parse(getter.downloadHandler.text);

				Debug.Log("--- START OF USERS ---");
				if (withID)
				{
					this.player.SetId(response["id"]);
					this.player.SetEmail(response["email"]);
					this.player.SetUsername(response["username"]);
					Debug.Log(this.player.GetUsername());
				}
				else
				{
					int userIndex = 0;
					while (response[userIndex]["username"] != null)
					{
						Debug.Log(response[userIndex]["username"]);
						User tempUser = new User();
						tempUser.SetId(response[userIndex]["id"]);
						tempUser.SetEmail(response[userIndex]["email"]);
						tempUser.SetUsername(response[userIndex]["username"]);
						//users.Add(tempUser);
						userIndex++;
					}
				}
				
				Debug.Log("--- END OF USERS ---");
			}
		}
	}

	public IEnumerator CreateUser(string createUrl, User createdUser)
	{
		using(UnityWebRequest post = createUserPostRequest(createUrl, createdUser.GetEmail(), createdUser.GetPassword()))
		{
			yield return post.SendWebRequest();
			if (post.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Something went wrong creating a user: " + post.error);
			}
			else
			{
				Debug.Log("User created successfully.");
				JSONNode success = JSON.Parse(post.downloadHandler.text);
				token = success["access_token"];

				StartCoroutine(GetUsers(Url, token, true, success["user"]["id"]));
			}
		}
	}

	public IEnumerator DeleteUser(string deleteUrl, string userToken, User userDelete)
	{
		deleteUrl += "user/" + userDelete.GetId();
		using(UnityWebRequest delete = createUserDeleteRequest(deleteUrl, userToken))
		{
			yield return delete.SendWebRequest();
			if (delete.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Something went wrong deleting a user: " + delete.error);
			}
			else
			{
				Debug.Log("User was deleted successfully.");
				StartCoroutine(GetUsers(Url, token));
			}
		}
	}

	public IEnumerator UpdateUser(string updateUrl, string userToken, User currentUser)
	{
		updateUrl += "user/" + currentUser.GetId();
		using(UnityWebRequest update = createUserUpdateRequest(updateUrl, userToken, currentUser))
		{
			yield return update.SendWebRequest();
			if (update.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Something went wrong updating a user: " + update.error);
			}
			else
			{
				Debug.Log("User was updated successfully.");
				StartCoroutine(GetUsers(Url, token, true, currentUser.GetId()));
			}
		}
	}

	private UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}

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
