using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

using SimpleJSON;

public class ImageFramesSpawner : MonoBehaviour
{
	public GameObject ClipFrameObject;

    static readonly string API_URL = "http://192.168.1.184:6996/clips/";

	UserDataReceiver userDataObj;

	void Start()
	{
		userDataObj = GameObject.FindObjectOfType<UserDataReceiver>();
		StartCoroutine(GetClipImages());
	}

	IEnumerator GetClipImages()
	{
		using(UnityWebRequest getImages = createGetRequest(API_URL, userDataObj.GetToken()))
		{
			yield return getImages.SendWebRequest();

			if (getImages.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Hey! Couldn't get the images: " + getImages.error);
			}
			else
			{
				Debug.Log("Info received:");
				JSONNode response = JSON.Parse(getImages.downloadHandler.text);
				int baseXCoord = -195;
				foreach (JSONNode clip in response)
				{
					ClipFrameObject.GetComponent<TrailerImgGetter>().Base64ToSprite(clip["clip_trailer_img"]);
					ClipFrameObject.transform.position = new Vector3(baseXCoord, 46.17f, -11.03f);
					baseXCoord += 2;
					Instantiate(ClipFrameObject, gameObject.transform);
				}

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
}
