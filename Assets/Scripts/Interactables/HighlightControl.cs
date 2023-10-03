using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightControl : MonoBehaviour
{
    [SerializeField] XRBaseInteractable interactableObject = null;
    [SerializeField] Material defaultMaterial = null;
    [SerializeField] Material emissionMaterial = null;
    [SerializeField] Renderer highlitableObject = null;
    [SerializeField] Renderer robotEye = null;
    [SerializeField] GameObject robotLight = null;

    private void OnEnable()
    {
        interactableObject.selectEntered.AddListener(HighlightObject);
        interactableObject.selectExited.AddListener(ResetObject);
    }

    private void OnDisable()
    {
        interactableObject.selectEntered.RemoveListener(HighlightObject);
        interactableObject.selectExited.RemoveListener(ResetObject);
    }

    private void HighlightObject(SelectEnterEventArgs arg0)
    {
        highlitableObject.material = emissionMaterial;
        robotEye.material = emissionMaterial;
        robotLight.SetActive(true);
    }

    private void ResetObject(SelectExitEventArgs arg0)
    {
        highlitableObject.material = defaultMaterial;
        robotEye.material = defaultMaterial;
        robotLight.SetActive(false);
    }
}
