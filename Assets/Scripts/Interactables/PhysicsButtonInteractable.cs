using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class PhysicsButtonInteractable : XRSimpleInteractable
{
    public UnityEvent OnBaseEnter;
    public UnityEvent OnBaseExit;

    [SerializeField] private Collider baseCollider = null;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHovered && other == baseCollider)
        {
            OnBaseEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == baseCollider)
        {
            OnBaseExit?.Invoke();
        }
    }
}
