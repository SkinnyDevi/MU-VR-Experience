using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;
using UserModel;

public class UserDataReceiver : MonoBehaviour
{
    public string Url, Email, Username, Password;

	string token = "";
	User player = new User();
	List<User> users = new List<User>();

	void Start()
	{
		player = new User();
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

	public void SetEmail(string email)
	{
		this.player.SetEmail(email);
		this.player.SetUsername(email.Split('@')[0].Replace(".", "_"));
	}

	public void SetPassword(string pwd)
	{
		this.player.SetEmail(pwd);
	}
	
	public void SetToken(string tkn)
	{
		token = tkn;
		Debug.Log(token);
	}

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
					this.player.SetPassword(response["password"]);
					this.player.SetPassword(response["email"]);
					this.player.SetUsername(response["username"]);
					this.player.SetAdminPrivileges(response["isAdmin"]);
					SetUserPublic(player);
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
						tempUser.SetPassword(response[userIndex]["password"]);
						tempUser.SetEmail(response[userIndex]["email"]);
						tempUser.SetUsername(response[userIndex]["username"]);
						tempUser.SetAdminPrivileges(response[userIndex]["isAdmin"]);
						users.Add(tempUser);
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

	private void SetUserPublic(User user)
	{
		this.Email = user.GetEmail();
		this.Password = user.GetPassword();
	}
}
