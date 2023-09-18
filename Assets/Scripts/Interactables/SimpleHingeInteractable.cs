using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SimpleHingeInteractable : XRSimpleInteractable
{
    [SerializeField] private bool isLocked = true;
    [SerializeField] private Vector3 positionLimits = Vector3.zero;

    private const string DEFAULT_LAYER = "Default";
    private const string GRAB_LAYER = "Grab";

    private Transform grabHand = null;
    private Collider hingeCollider = null;
    private Vector3 hingePositions = Vector3.zero;

    protected virtual void Start()
    {
        hingeCollider = GetComponent<Collider>();
    }

    protected virtual void Update()
    {
        if (grabHand)
        {
            TrackHand();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isLocked) return;

        base.OnSelectEntered(args);
        grabHand = args.interactorObject.transform;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        grabHand = null;
        ChangeLayerMask(GRAB_LAYER);
        ResetHinge();
    }

    private void TrackHand()
    {
        transform.LookAt(grabHand, transform.forward);
        hingePositions = hingeCollider.bounds.center;
        
        if (grabHand.position.x >= hingePositions.x + positionLimits.x || grabHand.position.x <= hingePositions.x - positionLimits.x)
        {
            ReleaseHinge();
        }
        else if (grabHand.position.y >= hingePositions.y + positionLimits.y || grabHand.position.y <= hingePositions.y - positionLimits.y)
        {
            ReleaseHinge();
        }
        else if (grabHand.position.z >= hingePositions.z + positionLimits.z || grabHand.position.z <= hingePositions.z - positionLimits.z)
        {
            ReleaseHinge();
        }
    }

    public void ReleaseHinge()
    {
        ChangeLayerMask(DEFAULT_LAYER);
    }

    protected abstract void ResetHinge();

    private void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void Lock()
    {
        isLocked = true;
    }
}
