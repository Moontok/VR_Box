using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WandControl : XRGrabInteractable
{
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] Transform projectileSpawnPoint = null;

    private bool isFiring = false;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
    }
}
