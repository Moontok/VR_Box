using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] CombinationLock comboLock = null;
    [SerializeField] Vector3 rotationLimts = Vector3.zero;
    [SerializeField] Transform doorObject = null;
    [SerializeField] Collider closeCollider = null;
    [SerializeField] Collider openCollider = null;
    [SerializeField] private Vector3 endRotation = Vector3.zero;

    private Vector3 startRotation = Vector3.zero;
    private float startAngleX = 0f;
    private bool isClosed = true;
    private bool isOpened = false;

    protected override void Start()
    {
        base.Start();
        startRotation = transform.localEulerAngles;
        startAngleX = GetAngle(startRotation.x);

        if (comboLock != null)
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

        if(isSelected) CheckLimits();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == closeCollider)
        {
            isClosed = true;
            ReleaseHinge();
        }
        else if (other == openCollider)
        {
            isOpened = true;
            ReleaseHinge();
        }
    }

    private void CheckLimits()
    {
        isClosed = false;
        isOpened = false;

        float localAngleX = GetAngle(transform.localEulerAngles.x);

        if (localAngleX >= startAngleX + rotationLimts.x || localAngleX <= startAngleX - rotationLimts.x)
        {
            ReleaseHinge();
        }
    }

    private float GetAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }

    private void OnUnlock()
    {
        Unlock();
    }

    private void OnLock()
    {
        Lock();
    }

    protected override void ResetHinge()
    {
        if (isClosed)
        {
            transform.localEulerAngles = startRotation;
        }
        else if (isOpened)
        {
            transform.localEulerAngles = endRotation;
        }
        else
        {
            transform.localEulerAngles = new Vector3(
                startAngleX,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z
            );
        }
    }
}
