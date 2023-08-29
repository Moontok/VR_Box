using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] CombonationLock comboLock = null;
    [SerializeField] Transform doorObject = null;

    private void Start()
    {
        if(comboLock != null)
        {
            comboLock.UnlockAction += OnUnlock;
            comboLock.LockAction += OnLock;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (doorObject != null)
        {
            doorObject.localEulerAngles = new Vector3(
                doorObject.localEulerAngles.x,
                transform.localEulerAngles.y,
                doorObject.localEulerAngles.z
            );
        }
    }

    private void OnUnlock()
    {
        Unlock();
    }

    private void OnLock()
    {
        Lock();
    }
}
