using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class XRButtonInteractable : XRSimpleInteractable
{
    [SerializeField] Image buttonImage = null;
    [SerializeField] Color[] buttonColors = new Color[4];

    private Color normalColor = Color.white;
    private Color highlightedColor = Color.white;
    private Color pressedColor = Color.white;
    private Color selectedColor = Color.white;
    private bool isPressed = false;

    private void Start()
    {
        normalColor = buttonColors[0];
        highlightedColor = buttonColors[1];
        pressedColor = buttonColors[2];
        selectedColor = buttonColors[3];

        buttonImage.color = normalColor;
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

        buttonImage.color = normalColor;
    }
}
