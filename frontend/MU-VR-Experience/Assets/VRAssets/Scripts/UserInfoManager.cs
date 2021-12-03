using UnityEngine;

using UserModel;

public static class UserInfoManager
{
    public enum Movement {WASD, Arrows}
    public enum Interaction {LeftClick, KeyE}
    public enum SaveType {SettingsMovement, SettingsInteraction, SettingsSensitivity, SettingsVolume}

    public static void SaveString(SaveType type, string str)
    {
        PlayerPrefs.SetString(type.ToString(), str);
        PlayerPrefs.Save();
    }

	public static void SaveString(string key, string str)
    {
        PlayerPrefs.SetString(key, str);
        PlayerPrefs.Save();
    }

    public static void SaveInt(SaveType type, int value)
    {
        PlayerPrefs.SetInt(type.ToString(), value);
        PlayerPrefs.Save();
    }

	public static void SaveFloat(SaveType type, float value)
	{
		PlayerPrefs.SetFloat(type.ToString(), value);
		PlayerPrefs.Save();
	}

	public static string GetString(SaveType type)
	{
		return PlayerPrefs.GetString(type.ToString());
	}

	public static string GetString(string str)
	{
		return PlayerPrefs.GetString(str);
	}

	public static int GetInt(SaveType type)
	{
		return PlayerPrefs.GetInt(type.ToString());
	}

	public static int GetInt(string str)
	{
		return PlayerPrefs.GetInt(str);
	}

	public static float GetFloat(SaveType type)
	{
		return PlayerPrefs.GetFloat(type.ToString());
	}

    public static void SaveUser(int id, string token)
    {
        PlayerPrefs.SetInt("User", id);
        PlayerPrefs.SetString("TempTKN", token);
        PlayerPrefs.Save();
    }

    public static void DeleteInfoOnExit()
    {
        PlayerPrefs.DeleteKey("User");
        PlayerPrefs.DeleteKey("TempTKN");
		PlayerPrefs.DeleteKey("videoID");
        PlayerPrefs.Save();
    }

    public static void ForceSave()
    {
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
		if (string.IsNullOrEmpty(PlayerPrefs.GetString("InitPrefs")))
		{
			SaveString(SaveType.SettingsMovement, Movement.WASD.ToString());
			SaveString(SaveType.SettingsInteraction, Interaction.LeftClick.ToString());
			SaveFloat(SaveType.SettingsSensitivity, 125f);
			SaveFloat(SaveType.SettingsVolume, 0f);
			PlayerPrefs.SetString("InitPrefs", "true");
		}
		else
		{
			SaveString(SaveType.SettingsMovement, GetString(SaveType.SettingsMovement));
			SaveString(SaveType.SettingsInteraction, GetString(SaveType.SettingsInteraction));
			SaveFloat(SaveType.SettingsSensitivity, GetFloat(SaveType.SettingsSensitivity));
			SaveFloat(SaveType.SettingsVolume, GetFloat(SaveType.SettingsVolume));
		}
		ForceSave();

		((VolumeSlider)(Resources.FindObjectsOfTypeAll(typeof(VolumeSlider))[0])).LoadVolume(GetFloat(SaveType.SettingsVolume));
		((SensitivitySlider)(Resources.FindObjectsOfTypeAll(typeof(SensitivitySlider))[0])).LoadSensitivity(GetFloat(SaveType.SettingsSensitivity));
    }
}