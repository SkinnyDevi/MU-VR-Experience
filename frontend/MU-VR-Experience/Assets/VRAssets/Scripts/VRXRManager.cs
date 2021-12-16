using UnityEngine;
using UnityEngine.XR.Management;

using System.Collections;

public class VRXRManager : MonoBehaviour
{
	bool _subsystemsStarted = false;

	void OnEnable()
	{
		if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            StartCoroutine(XRGeneralSettings.Instance.Manager.InitializeLoader());			
        }
	}

	void Update()
	{
		if (XRGeneralSettings.Instance.Manager.isInitializationComplete && !_subsystemsStarted)
		{
			// Debug.Log("Starting Subsystems");
			XRGeneralSettings.Instance.Manager.StartSubsystems();
			_subsystemsStarted = true;
			OVRManager.display.RecenterPose();
		}
	}

	void OnApplicationQuit() // for standalone only
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
