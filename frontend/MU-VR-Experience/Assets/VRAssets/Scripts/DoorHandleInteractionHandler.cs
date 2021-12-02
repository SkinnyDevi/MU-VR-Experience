using UnityEngine;

public class DoorHandleInteractionHandler : OVRGrabbable
{
    protected override void Start()
    {
        base.Start();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        Debug.Log(SceneLoader.Scene.TheatreBillboard);
        SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
