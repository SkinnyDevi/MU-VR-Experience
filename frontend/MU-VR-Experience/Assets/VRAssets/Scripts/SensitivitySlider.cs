using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SensitivitySlider : MonoBehaviour
{
    public Slider SensitivitySliderObject;
    public TMP_Text SensitivityValue;


    void Start()
    {
        SensitivitySliderObject.onValueChanged.AddListener((v) => {
            SensitivityValue.text = v.ToString();
        });
    }

	public void LoadSensitivity(float value)
	{
		SensitivitySliderObject.value = value;
		SensitivityValue.text = value.ToString("0");
		GameObject.FindObjectOfType<MouseLookControls>().MouseSensitivity = value;
	}
}
