using UnityEngine;
using UnityEngine.SceneManagement;

public class HandCollisionInteractionHandler : OVRGrabbable
{
    PointerControls rayCaster;

    protected override void Start()
    {
        base.Start();
        rayCaster = (PointerControls)(Resources.FindObjectsOfTypeAll(typeof(PointerControls))[0]);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        switch(SceneManager.GetActiveScene().name)
        {
            case "MainHub":
                SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
                break;
            case "TheatreBillboard":
                rayCaster.SetCurrentObject(grabPoint.gameObject.name);
                rayCaster.HandleBillboardEnterButtons();
                break;
            case "TheatreCinema":
                Debug.Log(grabPoint.gameObject.name);
                GameObject.Find("CustomHandRight").GetComponent<OVRGrabber>().ForceRelease(this);
                GameObject.Find("CustomHandLeft").GetComponent<OVRGrabber>().ForceRelease(this);
                rayCaster.SetCurrentObject(grabPoint.gameObject.name);
                rayCaster.HandleRatingButtons();
                break;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
