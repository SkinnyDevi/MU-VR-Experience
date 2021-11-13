using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public Slider VolumeSliderObject;
    public AudioMixer AudioMixer;
    public TMP_Text VolumeValue;

    void Start()
    {
        VolumeSliderObject.onValueChanged.AddListener((v) => {
            float volumePositive = ((v*-1/80*100)-100)*-1;
            VolumeValue.text = volumePositive.ToString("0");
            SetVolume(v);
        });
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);
    }
}
