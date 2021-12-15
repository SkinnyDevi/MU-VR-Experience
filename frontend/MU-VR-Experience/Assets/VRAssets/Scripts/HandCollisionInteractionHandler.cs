using UnityEngine;
using UnityEngine.SceneManagement;

public class HandCollisionInteractionHandler : OVRGrabbable
{
	public PointerControls VRRayCast;
	public Material InvisibleMaterial;

    PointerControls rayCaster;
	bool hasSubmittedRating = false;
	GameObject grabbedObj;

    protected override void Start()
    {
        base.Start();
		if (SceneManager.GetActiveScene().name.Equals("TheatreCinema"))	rayCaster = VRRayCast;
		else rayCaster = (PointerControls)(Resources.FindObjectsOfTypeAll(typeof(PointerControls))[0]);
	}

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
		base.GrabBegin(hand, grabPoint);
		grabbedObj = grabPoint.gameObject;
		rayCaster.SetCurrentObject(grabbedObj.name);
		Debug.Log(rayCaster.GetCurrentObject());
		switch(SceneManager.GetActiveScene().name)
		{
			case "MainHub":
				SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
				break;
			case "TheatreBillboard":
				rayCaster.HandleBillboardEnterButtons();
				break;
			case "TheatreCinema":
				if (grabbedObj.name.Contains("Cube"))
				{
					if (!hasSubmittedRating)
					{
						rayCaster.HandleRatingButtons();
						hasSubmittedRating = true;
						grabbedObj.GetComponent<Renderer>().material = InvisibleMaterial;
					}
				}
				break;
		}
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
		Debug.Log("STOPPED GRABBING: " + grabbedObj.name);
		if (SceneManager.GetActiveScene().name.Equals("TheatreCinema"))
		{
			if (grabbedObj.name.Contains("Cube") && hasSubmittedRating) GameObject.Find(grabbedObj.name).gameObject.SetActive(false);
			else SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
		}
		
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
