using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivitySlider : MonoBehaviour
{
    public Slider SensitivitySliderObject;
    public TMP_Text SensitivityValue;
    // Start is called before the first frame update
    void Start()
    {
        SensitivitySliderObject.onValueChanged.AddListener((v) => {
            SensitivityValue.text = v.ToString();
        });
    }
}
