using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRHandler : MonoBehaviour
{
    public enum PlayerType {Mouse, VR};
	public GameObject OVRPlayerObject, MousePlayerObject;
	public static PlayerType CurrentPlayerType = PlayerType.Mouse;

	Vector3 lobbyPlayerStartingCoords = new Vector3(-1451.133f, -286.0826f, 178.8876f);

	void Start()
	{
		
	}
	
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
