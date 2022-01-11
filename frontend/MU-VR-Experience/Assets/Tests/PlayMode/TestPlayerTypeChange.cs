using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerTypeChange
{
    [UnityTest]
    public IEnumerator PlayerTypeChanges()
    {
		var gameObject = new GameObject();
		var playerHandler = gameObject.AddComponent<PlayerVRHandler>();
		var simulatedVR = new GameObject();
		var simulatedMouse = new GameObject();

		playerHandler.OVRPlayerObject = simulatedVR;
		playerHandler.MousePlayerObject = simulatedMouse;

		playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.VR);

        yield return new WaitForSeconds(0.1f);

		Assert.AreEqual(PlayerVRHandler.PlayerType.VR, PlayerVRHandler.CurrentPlayerType);
		Assert.AreEqual(false, simulatedMouse.activeSelf);
		Assert.AreEqual(true, simulatedVR.activeSelf);
    }
}
