using UnityEngine;
using UnityEngine.Video;

using System.Collections;

public class VideoLoader : MonoBehaviour
{
    VideoPlayer screenPlayer;
	AudioSource screenAudio;

    void Start()
    {
        Application.runInBackground = true;
    	StartCoroutine(LoadVideo());
    }

	IEnumerator LoadVideo()
	{
		screenPlayer = GetComponent<VideoPlayer>();
		screenAudio = screenPlayer.transform.Find("Video Sound").gameObject.GetComponent<AudioSource>();

		screenPlayer.playOnAwake = false; 
		screenAudio.playOnAwake = false;

		screenPlayer.clip = Resources.Load<VideoClip>("videos/" + UserInfoManager.GetInt("VideoID")) as VideoClip;

		screenPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		screenPlayer.EnableAudioTrack(0, true);
		screenPlayer.SetTargetAudioSource(0, screenAudio);

		screenPlayer.Prepare();

		while (!screenPlayer.isPrepared)
		{
			// Debug.Log("Preparing video... WAIT!");
			yield return null;
		}

		// Debug.Log("Video was loaded.");

		screenPlayer.Play();
		screenAudio.Play();

		// Debug.Log("Playing Video");
		while (screenPlayer.isPlaying)
		{
			//// Debug.Log("Video Time: " + Mathf.FloorToInt((float)screenPlayer.time));
			yield return null;
		}

		// Debug.Log("Done Playing Video");
	}	
}
