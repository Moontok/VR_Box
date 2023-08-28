using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class CombonationLock : MonoBehaviour
{
    [SerializeField] private TMP_Text userInputText = null;
    [SerializeField] private XRButtonInteractable[] comboButtons = null;
    [SerializeField] private Image lockedPanel = null;
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private TMP_Text lockedText = null;
    [SerializeField] private string lockedTextString = "Locked";
    [SerializeField] private string unlockedTextString = "Unlocked";
    [SerializeField] private bool isLocked = true;
    [SerializeField] private string comboCode = "000";

    string inputValues = "";
    int maxButtonPresses = 0;
    int buttonPresses = 0;


    private void Start()
    {
        maxButtonPresses = comboCode.Length;
        ResetLock();

        foreach (XRButtonInteractable button in comboButtons)
        {
            button.selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs args)
    {
        if (!isLocked) return;

        if (buttonPresses >= maxButtonPresses)
        {
            ResetLock();
        }

        for (int i = 0; i < comboButtons.Length; i++)
        {
            if (args.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                inputValues += i.ToString();
                userInputText.text = inputValues;
            }
            else
            {
                comboButtons[i].ResetColor();
            }
        }
        buttonPresses++;
        if (buttonPresses == maxButtonPresses)
        {
            CheckCombo();
        }
    }

    private void CheckCombo()
    {
        if (comboCode == inputValues)
        {
            isLocked = false;
            lockedPanel.color = unlockedColor;
            lockedText.text = unlockedTextString;
        }
    }

    private void ResetLock()
    {
        inputValues = "";
        userInputText.text = "";
        isLocked = true;
        lockedPanel.color = lockedColor;
        lockedText.text = lockedTextString;
        buttonPresses = 0;
    }
}
