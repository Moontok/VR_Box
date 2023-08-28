using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleHingeInteractable : XRSimpleInteractable
{
    private Transform grabHand = null;

    protected virtual void Update()
    {
        if (grabHand)
        {
            transform.LookAt(grabHand, transform.forward);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        grabHand = args.interactorObject.transform;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        grabHand = null;
    }
}
