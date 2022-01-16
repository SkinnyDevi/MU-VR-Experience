using UnityEngine;
using UnityEngine.Networking;

using WebSocketSharp;

using System.Collections;

using TMPro;

using SimpleJSON;

public class RatingPercentageHandler : MonoBehaviour
{
	public TMP_Text LikedPercentage, RegularPercentage, DislikedPercentage;

    const string API_URL = "http://localhost:6996/ratings/by_type/";
	const string WS_URL = "ws://localhost:6996/ratings";
    int _clipId = -1;
    string _token, _updatedRatings = "";
	WebSocket _ws;
	TMP_Text _likedText, _regularText, _dislikedText;
	bool _allowUpdate = false;

	void Start()
	{
		_ws = new WebSocket(WS_URL);
        _ws.Connect();
        _ws.OnMessage += (sender, e) =>
        {
			JSONNode response = JSON.Parse(e.Data);
			if(response["returned_payload"]["clip_id"] == _clipId)
			{
				Debug.Log($"Frame Clip ID:{_clipId}, RPH - Theres new info for me");
				_updatedRatings = (response["returned_payload"]["updated_ratings"]).ToString();
				_allowUpdate = true;
			}
        };
	}

	void Update()
	{
		if (_allowUpdate)
		{
			SetRatingsToText(_updatedRatings);
		}
	}

    public void SetRequestInfo(int id, string tkn)
    {
        _clipId = id;
        _token = tkn;
        StartCoroutine(CollectClipRatings());
    }

	private void GenerateRatings()
	{
		LikedPercentage.gameObject.name = "Like-" + _clipId;
		RegularPercentage.gameObject.name = "Regular-" + _clipId;
		DislikedPercentage.gameObject.name = "Dislike-" + _clipId;

		_likedText = Instantiate(LikedPercentage, gameObject.transform);
		_regularText = Instantiate(RegularPercentage, gameObject.transform);
		_dislikedText = Instantiate(DislikedPercentage, gameObject.transform);
	}

    private IEnumerator CollectClipRatings()
    {
        using(UnityWebRequest ratings = createGetRequest(API_URL + _clipId, _token))
        {
            yield return ratings.SendWebRequest();

            if (ratings.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Couldn't complete ratings type request: " + ratings.error);
            }
            else
            {
				GenerateRatings();
                SetRatingsToText(ratings.downloadHandler.text);
            }
        }
    }

    private string GetPercentage(float x, float y)
    {
        return ((x/y)*100.0).ToString("0.0") + "%";
    }

	private void SetRatingsToText(string res)
	{
		float liked, regulars, disliked, total = 0;
		JSONNode response = JSON.Parse(res);
        liked = response["Liked"].Count;
        regulars = response["Regular"].Count;
        disliked = response["Disliked"].Count;
        total = liked + disliked + regulars;

		_likedText.text = GetPercentage(liked, total);
        _regularText.text = GetPercentage(regulars, total);
        _dislikedText.text = GetPercentage(disliked, total);

		_allowUpdate = false;
	}

    private UnityWebRequest createGetRequest(string requestUrl, string bearerToken)
	{
		UnityWebRequest getUsers = UnityWebRequest.Get(requestUrl);
		getUsers.SetRequestHeader("Content-Type", "application/json");
		getUsers.SetRequestHeader("Authorization", "Bearer " + bearerToken); 

		return getUsers;
	}
}
