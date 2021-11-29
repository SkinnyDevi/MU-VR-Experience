using UnityEngine;

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

    public static void SaveInt(SaveType type, int value)
    {
        PlayerPrefs.SetInt(type.ToString(), value);
        PlayerPrefs.Save();
    }

    public static void SaveUser(string username, string token)
    {
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("TempTKN", token);
        PlayerPrefs.Save();
    }

    public static void DeleteUser()
    {
        PlayerPrefs.DeleteKey("Username");
        PlayerPrefs.DeleteKey("TempTKN");
        PlayerPrefs.Save();
    }

    public static void ForceSave()
    {
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        PlayerPrefs.SetString(SaveType.SettingsMovement.ToString(), PlayerPrefs.GetString(SaveType.SettingsMovement.ToString()));
        PlayerPrefs.SetString(SaveType.SettingsInteraction.ToString(), PlayerPrefs.GetString(SaveType.SettingsInteraction.ToString()));
        PlayerPrefs.SetInt(SaveType.SettingsSensitivity.ToString(), PlayerPrefs.GetInt(SaveType.SettingsSensitivity.ToString()));
        PlayerPrefs.SetInt(SaveType.SettingsVolume.ToString(), PlayerPrefs.GetInt(SaveType.SettingsVolume.ToString()));
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString(SaveType.SettingsMovement.ToString()));
        Debug.Log(PlayerPrefs.GetString(SaveType.SettingsInteraction.ToString()));
        Debug.Log(PlayerPrefs.GetInt(SaveType.SettingsSensitivity.ToString()));
        Debug.Log(PlayerPrefs.GetInt(SaveType.SettingsVolume.ToString()));

    }
}