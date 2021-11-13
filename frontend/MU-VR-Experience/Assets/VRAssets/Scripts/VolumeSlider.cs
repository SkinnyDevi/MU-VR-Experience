using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public Slider VolumeSliderObject;
    public AudioMixer AudioMixer;
    public TMP_Text VolumeValue;

    // Start is called before the first frame update
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
