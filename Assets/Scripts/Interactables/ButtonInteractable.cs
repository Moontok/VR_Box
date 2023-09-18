using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ButtonInteractable : XRSimpleInteractable
{
    [SerializeField] Image buttonImage = null;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.white;
    [SerializeField] private Color pressedColor = Color.white;
    [SerializeField] private Color selectedColor = Color.white;
    private bool isPressed = false;

    private void Start()
    {
        ResetColor();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isPressed = true;
        buttonImage.color = pressedColor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isPressed = false;
        buttonImage.color = selectedColor;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isPressed = false;
        buttonImage.color = highlightedColor;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (isPressed) return;

        ResetColor();
    }

    public void ResetColor()
    {
        buttonImage.color = normalColor;
    }
}
