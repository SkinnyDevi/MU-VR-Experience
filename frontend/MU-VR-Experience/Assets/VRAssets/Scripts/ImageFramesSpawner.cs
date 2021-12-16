using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

using TMPro;

using SimpleJSON;

public class ImageFramesSpawner : MonoBehaviour
{
	public GameObject ClipFrameObject, EnterButtonObject;

    static readonly string API_URL = "http://localhost:6996/clips/";

	UserDataReceiver userDataObj;

	void Start()
	{
		userDataObj = GameObject.FindObjectOfType<UserDataReceiver>();
	}

	public void LoadImageFrames()
	{
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
				JSONNode response = JSON.Parse(getImages.downloadHandler.text);
				int baseXCoord = -195;
				int frameCounts = 0;
				foreach (JSONNode clip in response)
				{
					if (frameCounts < 9) {
						ClipFrameObject.GetComponent<TrailerImgGetter>().Base64ToSprite(clip["clip_trailer_img"]);
						ClipFrameObject.transform.position = new Vector3(baseXCoord, 46.17f, -11.03f);
						ClipFrameObject.transform.Find("Canvas/ClipName").GetComponent<TMP_Text>().text = clip["clip_name"];
						baseXCoord += 2;
						GameObject newFrame = Instantiate(ClipFrameObject, gameObject.transform);
						newFrame.transform.Find("Canvas/Rating Text").GetComponent<RatingPercentageHandler>().SetRequestInfo(clip["clip_id"], userDataObj.GetToken());
						EnterButtonObject.name = "Enter-" + clip["clip_id"];
						Instantiate(EnterButtonObject, newFrame.transform);
						frameCounts++;
					}
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
