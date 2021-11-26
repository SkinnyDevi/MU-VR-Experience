using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

using SimpleJSON;

public class TrailerImgGetter : MonoBehaviour
{
	public int ClipID = 1;
	static readonly string API_URL = "http://192.168.1.184:6996/clips/clip/";
    //TODO: Grab image from DB and place inside 2D Sprite inside frame

	SpriteRenderer trailerImg;
	UserDataReceiver userDataObj;

	void Start()
	{
		trailerImg = gameObject.transform.Find("TrailerImg").GetComponent<SpriteRenderer>();
		userDataObj = GameObject.FindObjectOfType<UserDataReceiver>();
		StartCoroutine(RetrieveImage());
	}

	IEnumerator RetrieveImage()
	{
		using(UnityWebRequest getImage = createGetRequest(API_URL + ClipID, userDataObj.GetToken()))
		{
			yield return getImage.SendWebRequest();

			if (getImage.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Hey! Couldn't get the image: " + getImage.error);
			}
			else
			{
				Debug.Log("Info received:");
				Debug.Log(getImage.downloadHandler.text);
				JSONNode response = JSON.Parse(getImage.downloadHandler.text);
				trailerImg.sprite = Base64ToImage(response["clip_trailer_img"]);
			}
		}
	}

	private Sprite Base64ToImage(string baseString)
	{
		byte[] imageBytes = Convert.FromBase64String(baseString);
		Texture2D texture = new Texture2D(2, 2);
		texture.LoadImage(imageBytes);
		return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
	}

	private UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}
}
