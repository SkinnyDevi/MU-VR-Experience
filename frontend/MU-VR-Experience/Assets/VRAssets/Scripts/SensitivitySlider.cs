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
}
