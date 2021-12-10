using UnityEngine;
using UnityEngine.SceneManagement;

public class HandCollisionInteractionHandler : OVRGrabbable
{
    PointerControls rayCaster;
	PlayerVRHandler playerHandler;

    protected override void Start()
    {
        base.Start();
        rayCaster = (PointerControls)(Resources.FindObjectsOfTypeAll(typeof(PointerControls))[0]);
		playerHandler = GameObject.FindObjectOfType<PlayerVRHandler>();
	}

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
		Debug.Log(rayCaster.GetCurrentObject());
		rayCaster.SetCurrentObject(grabPoint.gameObject.name);
		if (!rayCaster.GetCurrentObject().Equals("Remove VR Variant(Clone)"))
		{
			switch(SceneManager.GetActiveScene().name)
			{
				case "MainHub":
					SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
					break;
				case "TheatreBillboard":
					rayCaster.HandleBillboardEnterButtons();
					break;
				case "TheatreCinema":
					Debug.Log(grabPoint.gameObject.name);
					GameObject.Find("CustomHandRight").GetComponent<OVRGrabber>().ForceRelease(this);
					GameObject.Find("CustomHandLeft").GetComponent<OVRGrabber>().ForceRelease(this);	
					rayCaster.HandleRatingButtons();
					break;
			}
		}
		else
		{
			GameObject.FindObjectOfType<SettingsMenu>().ExternalVRDeactivation();
			playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.Mouse);
			Destroy(grabPoint.gameObject);
		}	
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
