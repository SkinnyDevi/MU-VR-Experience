using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    VideoPlayer screenPlayer;

    void Start()
    {
        screenPlayer = GetComponent<VideoPlayer>();
        screenPlayer.clip = Resources.Load<VideoClip>("videos/" + UserInfoManager.GetInt("VideoID")) as VideoClip;
    }
}
