using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
	VideoPlayer screenPlayer;

    void Start()
    {
		screenPlayer = GetComponent<VideoPlayer>();
		screenPlayer.url = UserInfoManager.GetString("VideoURL");
    }
}
