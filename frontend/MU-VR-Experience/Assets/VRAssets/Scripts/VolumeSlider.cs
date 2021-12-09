using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public Slider VolumeSliderObject;
    public AudioMixer AudioMixer;
	public static float RawVolume = 0f;
    public TMP_Text VolumeValue;

    // void Start()
    // {
	// 	AudioMixer = Instantiate(AudioMixer);
    //     VolumeSliderObject.onValueChanged.AddListener((v) => {
    //         float volumePositive = ((v*-1/80*100)-100)*-1;
    //         VolumeValue.text = volumePositive.ToString("0");
    //         SetVolume(v);
    //     });
    // }

    public void SetVolume(float volume)
    {
		RawVolume = volume;
		float volumePositive = ((volume*-1/80*100)-100)*-1;
        VolumeValue.text = volumePositive.ToString("0");
        AudioMixer.SetFloat("MasterVolume", volume);
    }

	public void LoadVolume(float newVol)
	{
		SetVolume(newVol);
		VolumeSliderObject.value = newVol;
		VolumeValue.text = (((newVol*-1/80*100)-100)*-1).ToString("0");
	}
}
