using UnityEngine;
using UnityEngine.XR.Management;

using System.Collections;

public class VRXRManager : MonoBehaviour
{
	bool subsystemsStarted = false;

	void OnEnable()
	{
		if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            StartCoroutine(XRGeneralSettings.Instance.Manager.InitializeLoader());			
        }
	}

	void Update()
	{
		if (XRGeneralSettings.Instance.Manager.isInitializationComplete && !subsystemsStarted)
		{
			Debug.Log("Starting Subsystems");
			XRGeneralSettings.Instance.Manager.StartSubsystems();
			subsystemsStarted = true;
		}
	}

	void OnApplicationQuit() // for standalone only
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
