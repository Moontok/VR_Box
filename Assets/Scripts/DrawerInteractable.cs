using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerInteractable : XRGrabInteractable
{
    [SerializeField] Transform drawerTransform = null;
    [SerializeField] XRSocketInteractor keySocket = null;
    [SerializeField] bool isLocked = true;

    private Transform parentTransform = null;
    private bool isGrabbed = false;
    private const string DEFAULT_LAYER = "Default";
    private const string GRAB_LAYER = "Grab";

    void Start()
    {
        if (keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }

        parentTransform = transform.parent;
    }

    void Update()
    {
        if (isGrabbed && drawerTransform != null)
        {
            drawerTransform.localPosition = new Vector3(
                drawerTransform.localPosition.x,
                drawerTransform.localPosition.y,
                transform.localPosition.z
            );
        }
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs args)
    {
        isLocked = false;
    }

    private void OnDrawerLocked(SelectExitEventArgs args)
    {
        isLocked = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (isLocked)
        {
              interactionLayers = InteractionLayerMask.GetMask(DEFAULT_LAYER);
        }
        else
        {
            transform.SetParent(parentTransform);
            isGrabbed = true;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactionLayers = InteractionLayerMask.GetMask(GRAB_LAYER);
        isGrabbed = false;
    }
}
