using UnityEngine;
using UnityEngine.SceneManagement;

public class HandCollisionInteractionHandler : OVRGrabbable
{
	public PointerControls VRRayCast;
	public Material InvisibleMaterial;

    PointerControls _rayCaster;
	bool _hasSubmittedRating = false;
	GameObject _grabbedObj;

    protected override void Start()
    {
        base.Start();
		if (SceneManager.GetActiveScene().name.Equals("TheatreCinema"))	_rayCaster = VRRayCast;
		else _rayCaster = (PointerControls)(Resources.FindObjectsOfTypeAll(typeof(PointerControls))[0]);
	}

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
		base.GrabBegin(hand, grabPoint);
		_grabbedObj = grabPoint.gameObject;
		_rayCaster.SetCurrentObject(_grabbedObj.name);
		// Debug.Log(_rayCaster.GetCurrentObject());
		switch(SceneManager.GetActiveScene().name)
		{
			case "MainHub":
				SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
				break;
			case "TheatreBillboard":
				_rayCaster.HandleBillboardEnterButtons();
				break;
			case "TheatreCinema":
				if (_grabbedObj.name.Contains("Cube"))
				{
					if (!_hasSubmittedRating)
					{
						_rayCaster.HandleRatingButtons();
						_hasSubmittedRating = true;
						_grabbedObj.GetComponent<Renderer>().material = InvisibleMaterial;
					}
				}
				break;
		}
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
		// Debug.Log("STOPPED GRABBING: " + _grabbedObj.name);
		if (SceneManager.GetActiveScene().name.Equals("TheatreCinema"))
		{
			if (_grabbedObj.name.Contains("Cube") && _hasSubmittedRating) GameObject.Find(_grabbedObj.name).gameObject.SetActive(false);
			else SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
		}
		
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
