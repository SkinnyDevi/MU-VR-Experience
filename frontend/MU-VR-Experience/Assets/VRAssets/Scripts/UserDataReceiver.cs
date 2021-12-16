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

	const string API_URL = "http://192.168.1.184:6996/users/user/";

	string _token = "";
	User _player = new User();

	void Start()
	{
		_player = new User();
		UserInfoManager.LoadSettings();
		if (!SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
		{
			StartCoroutine(LoadNewPlayer());
		}
	}

	void OnApplicationQuit() // Only for standalone
	{
		UserInfoManager.DeleteInfoOnExit();
		UserInfoManager.SavePlayerType(UserInfoManager.PlayerType.Mouse);
	}

	public User CurrentPlayer()
	{
		return this._player;
	}

	public void SetCurrentPlayer(User newPlayer)
	{
		this._player = newPlayer;

		if (SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
		{
			GameObject.FindObjectOfType<UserTransition>().TriggerSuccessExit();
			bool currentObject = Crosshair.GetComponent<PointerControls>().GetCurrentObject().Equals("Login");
			if (currentObject) LoginExitButton.onClick.Invoke();
			else RegisterExitButton.onClick.Invoke();
		}

		SettingsUserText.text = "User: " + this._player.GetUsername();
		SettingsUserText.gameObject.SetActive(true);
	}

	private IEnumerator LoadNewPlayer()
	{
		using(UnityWebRequest getUser = CreateGetRequest(API_URL + UserInfoManager.GetInt("User"), UserInfoManager.GetString("TempTKN")))
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
		_token = tkn;
		if (SceneManager.GetActiveScene().name.Equals(SceneLoader.Scene.MainHub.ToString()))
			GameObject.Find("Environment/RegisterRoom/Walls/BillBoardEntry").SetActive(true);
		UserInfoManager.SaveUser(this._player.GetId(), tkn);
	}

	public string GetToken()
	{
		return _token;
	}

	private UnityWebRequest CreateGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}
}
