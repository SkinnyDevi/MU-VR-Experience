using UnityEngine;
using System.Collections.Generic;

public class CustomHandGrabber : OVRGrabber
{
	protected override void Start()
	{
		base.Start();
	}

	public void ForceRelease()
	{
		base.GrabEnd();
	}
}