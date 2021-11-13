using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsOptionsChooser : MonoBehaviour
{
    public Button Option1Button;
    public Button Option2Button;
	public TMP_Text Option1TextMesh;
    public TMP_Text Option2TextMesh;

    void Start()
    {
        Select1();
    }

    public void Select1()
    {
        Option1Button.interactable = false;
        Option1TextMesh.fontStyle = FontStyles.Underline | FontStyles.Italic;
    }

    public void Deselect1()
    {
        Option1Button.interactable = true;
        Option1TextMesh.fontStyle = FontStyles.Normal;
    }

    public void Select2()
    {
        Option2Button.interactable = false;
        Option2TextMesh.fontStyle = FontStyles.Underline | FontStyles.Italic;
    }

    public void Deselect2()
    {
        Option2Button.interactable = true;
        Option2TextMesh.fontStyle = FontStyles.Normal;
    }
}
