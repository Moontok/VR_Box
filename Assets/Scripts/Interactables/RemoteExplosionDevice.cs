using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class RemoteExplosionDevice : XRGrabInteractable
{
    [SerializeField] TheWall wallToExplode = null;

    private bool isActivated = false;

    public void Activate(bool value)
    {
        isActivated = value;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isActivated && other.gameObject.GetComponent<WandProjectile>() != null)
        {
            wallToExplode.ExplodeWall();
        }
    }
}
