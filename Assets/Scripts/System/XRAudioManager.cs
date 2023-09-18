using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRAudioManager : MonoBehaviour
{
    [Header("Grab Interactables")]
    [SerializeField] XRGrabInteractable[] grabInteractables = null;
    [SerializeField] AudioSource grabSoundSource = null;
    [SerializeField] AudioClip grabClip = null;
    [SerializeField] AudioClip keyClip = null;
    [SerializeField] AudioSource activateSoundSource = null;
    [SerializeField] AudioClip grabActivatedClip = null;
    [SerializeField] AudioClip wandActivatedClip = null;

    [Header("Drawer Interactable")]
    [SerializeField] DrawerInteractable drawer = null;
    [SerializeField] AudioSource drawerSoundSource = null;
    [SerializeField] AudioClip drawerMoveClip = null;

    [Header("The Wall")]
    [SerializeField] TheWall wall = null;
    [SerializeField] AudioSource wallSource = null;

    private void OnEnable()
    {
        foreach (var grabInteractable in grabInteractables)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.activated.AddListener(OnActivate);
        }
        wall?.OnDestroy.AddListener(OnDestroyWall);
        
    }

    private void OnDisable()
    {
        wall?.OnDestroy.RemoveListener(OnDestroyWall);
    }

    private void OnDestroyWall()
    {
        wallSource.Play();
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Key")) grabSoundSource.PlayOneShot(keyClip);
        else grabSoundSource.PlayOneShot(grabClip);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        grabSoundSource.PlayOneShot(grabClip);
    }

    private void OnActivate(ActivateEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Wand")) activateSoundSource.PlayOneShot(wandActivatedClip);
    }
}
