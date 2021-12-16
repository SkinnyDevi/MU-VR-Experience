using UnityEngine;

public class PlayerVRHandler : MonoBehaviour
{
    public enum PlayerType {Mouse, VR};
	public GameObject OVRPlayerObject, MousePlayerObject;
	public static PlayerType CurrentPlayerType = PlayerType.Mouse;
	
	public void ChangePlayerType(PlayerType type)
	{
		CurrentPlayerType = type;
		TransitionToPlayerType();
	}

	void TransitionToPlayerType()
	{
		if (CurrentPlayerType == PlayerType.VR)
		{
			MousePlayerObject.SetActive(false);
			OVRPlayerObject.SetActive(true);
		}
		else
		{
			OVRPlayerObject.SetActive(false);
			MousePlayerObject.SetActive(true);
		}
	}
}
