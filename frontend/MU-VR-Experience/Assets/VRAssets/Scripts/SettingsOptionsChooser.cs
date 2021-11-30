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
        LoadConfig();
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

	public void LoadConfig()
	{
		if (gameObject.name.Equals("Movement Choose"))
		{
			if (UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement).Equals(UserInfoManager.Movement.WASD.ToString()))
			{
				Select1();
				Deselect2();
			}
			else
			{
				Select2();
				Deselect1();
			}
		}
		else
		{
			if (UserInfoManager.GetString(UserInfoManager.SaveType.SettingsInteraction).Equals(UserInfoManager.Interaction.LeftClick.ToString()))
			{
				Select1();
				Deselect2();
			}
			else
			{
				Select2();
				Deselect1();
			}
		}
	}
}
