using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class DrawerInteractable : XRGrabInteractable
{
    public UnityEvent OnDrawerDetached;

    [SerializeField] Transform drawerTransform = null;
    [SerializeField] XRSocketInteractor keySocket = null;
    [SerializeField] PhysicsButtonInteractable physicsButton = null;
    [SerializeField] bool isLocked = true;
    [SerializeField] bool isDetachable = false;
    [SerializeField] bool isDetached = false;
    [SerializeField] Vector3 distanceLimits = new Vector3(0.1f, 0.1f, 0f);
    [SerializeField] float drawerZLimit = 0.8f;
    [SerializeField] GameObject keyLight = null;
    [SerializeField] GameObject particles = null;
    [SerializeField] MeshRenderer[] keyParts = null;
    [SerializeField] Material keyBaseMat = null;
    [SerializeField] AudioClip drawerMoveClip = null;

    private Transform parentTransform = null;
    private bool isGrabbed = false;
    private Vector3 positionLimits = Vector3.zero;
    private Rigidbody rb = null;

    private const string DEFAULT_LAYER = "Default";
    private const string GRAB_LAYER = "Grab";

    public AudioClip GetDrawerMoveClip => drawerMoveClip;
    public XRSocketInteractor GetKeySocket => keySocket;
    public PhysicsButtonInteractable GetPhysicsButton => physicsButton;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }

        parentTransform = transform.parent;
        positionLimits = drawerTransform.localPosition;

        physicsButton?.OnBaseEnter.AddListener(OnIsDetachable);
        physicsButton?.OnBaseExit.AddListener(OnIsNotDetachable);
    }

    void Update()
    {
        if (isDetached) return;

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

    private void OnIsDetachable()
    {
        isDetachable = true;
    }

    private void OnIsNotDetachable()
    {
        isDetachable = false;
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
            if (!isDetachable)
            {
                isGrabbed = false;
                drawerTransform.localPosition = new Vector3(
                    drawerTransform.localPosition.x,
                    drawerTransform.localPosition.y,
                    drawerZLimit
                );
                ChangeLayerMask(DEFAULT_LAYER);
            }
            else
            {
                DetachDrawer();
            }
        }
    }

    private void DetachDrawer()
    {
        isDetached = true;
        drawerTransform.SetParent(this.transform);
        OnDrawerDetached?.Invoke();
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

        if (!isDetached)
        {
            ChangeLayerMask(GRAB_LAYER);
            isGrabbed = false;
            transform.localPosition = drawerTransform.localPosition;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    private void TurnKeyLightOff()
    {
        keyLight.SetActive(false);
        particles.SetActive(false);
        foreach (MeshRenderer keyPart in keyParts)
        {
            keyPart.material = keyBaseMat;
        }
    }
}
