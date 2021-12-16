using UnityEngine;
using UnityEngine.Video;

using System.Collections;

public class VideoLoader : MonoBehaviour
{
    VideoPlayer _screenPlayer;
	AudioSource _screenAudio;

    void Start()
    {
        Application.runInBackground = true;
    	StartCoroutine(LoadVideo());
    }

	private IEnumerator LoadVideo()
	{
		_screenPlayer = GetComponent<VideoPlayer>();
		_screenAudio = _screenPlayer.transform.Find("Video Sound").gameObject.GetComponent<AudioSource>();

		_screenPlayer.playOnAwake = false; 
		_screenAudio.playOnAwake = false;

		_screenPlayer.clip = Resources.Load<VideoClip>("videos/" + UserInfoManager.GetInt("VideoID")) as VideoClip;

		_screenPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		_screenPlayer.EnableAudioTrack(0, true);
		_screenPlayer.SetTargetAudioSource(0, _screenAudio);

		_screenPlayer.Prepare();

		while (!_screenPlayer.isPrepared)
		{
			// Debug.Log("Preparing video... WAIT!");
			yield return null;
		}

		// Debug.Log("Video was loaded.");

		_screenPlayer.Play();
		_screenAudio.Play();

		// Debug.Log("Playing Video");
		while (_screenPlayer.isPlaying)
		{
			//// Debug.Log("Video Time: " + Mathf.FloorToInt((float)_screenPlayer.time));
			yield return null;
		}

		// Debug.Log("Done Playing Video");
	}	
}
