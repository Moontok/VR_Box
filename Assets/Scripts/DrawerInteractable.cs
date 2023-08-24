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
    [SerializeField] Vector3 distanceLimits = new Vector3(0.1f, 0.1f, 0f);
    [SerializeField] float drawerZLimit = 0.8f;
    [SerializeField] GameObject keyLight = null;
    [SerializeField] GameObject particles = null;
    [SerializeField] GameObject[] keyParts = null;

    private Transform parentTransform = null;
    private bool isGrabbed = false;
    private Vector3 positionLimits = Vector3.zero;

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
        positionLimits = drawerTransform.localPosition;
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

            CheckLimits();
        }
    }

    private void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }

    private void CheckLimits()
    {
        if (
            transform.localPosition.x > positionLimits.x + distanceLimits.x ||
            transform.localPosition.x < positionLimits.x - distanceLimits.x ||
            transform.localPosition.y > positionLimits.y + distanceLimits.y ||
            transform.localPosition.y < positionLimits.y - distanceLimits.y
        )    
        {
            ChangeLayerMask(DEFAULT_LAYER);
        }
        else if (drawerTransform.localPosition.z < positionLimits.z - distanceLimits.z)
        {
            isGrabbed = false;
            drawerTransform.localPosition = positionLimits;
            ChangeLayerMask(DEFAULT_LAYER);
        }
        else if (drawerTransform.localPosition.z > drawerZLimit + distanceLimits.z)
        {
            isGrabbed = false;
            drawerTransform.localPosition = new Vector3(
                drawerTransform.localPosition.x,
                drawerTransform.localPosition.y,
                drawerZLimit
            );
            ChangeLayerMask(DEFAULT_LAYER);
        }
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs args)
    {
        isLocked = false;
        if (keyLight != null)
        {
            TurnKeyLightOff();
        }
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
            ChangeLayerMask(DEFAULT_LAYER);
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
        ChangeLayerMask(GRAB_LAYER);
        isGrabbed = false;
        transform.localPosition = drawerTransform.localPosition;
    }

    private void TurnKeyLightOff()
    {
        keyLight.SetActive(false);
        particles.SetActive(false);
        foreach (GameObject keyPart in keyParts)
        {
            keyPart.GetComponent<MaterialController>().TurnEmissionOff();
        }
    }
}
