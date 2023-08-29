using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CombonationLock : MonoBehaviour
{
    public UnityAction UnlockAction;
    private void OnUnlock() => UnlockAction?.Invoke();
    public UnityAction LockAction;
    private void OnLock() => LockAction?.Invoke();

    [SerializeField] private TMP_Text userInputText = null;
    [SerializeField] private SymbolButtonInteractable[] comboButtons = null;
    [SerializeField] private string comboCode = "000";
    [SerializeField] private TMP_Text infoText = null;
    [SerializeField] private string infoStartString = "Enter 3 Digit Code";
    [SerializeField] private string infoResetString = "Enter 3 Digits to Reset";
    [SerializeField] private Image lockedPanel = null;
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private TMP_Text lockedText = null;
    [SerializeField] private string lockedTextString = "Locked";
    [SerializeField] private string unlockedTextString = "Unlocked";
    [SerializeField] private bool isLocked = true;
    [SerializeField] private bool isResettable = true;

    string inputValues = "";
    int maxButtonPresses = 0;
    int buttonPresses = 0;
    bool resetCombo = false;


    private void Start()
    {
        maxButtonPresses = comboCode.Length;
        ResetUserValues();

        foreach (SymbolButtonInteractable button in comboButtons)
        {
            button.selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs args)
    {
        if (!isLocked && !isResettable) return;

        if (buttonPresses >= maxButtonPresses)
        {
            ResetUserValues();
        }

        string symbol = args.interactableObject.transform.GetComponent<SymbolButtonInteractable>().Symbol;
        userInputText.text += symbol;
        inputValues += symbol;
        buttonPresses++;

        if (buttonPresses == maxButtonPresses)
        {
            CheckCombo();
        }
    }

    private void CheckCombo()
    {
        if (resetCombo)
        {
            resetCombo = false;
            LockCombo();
            return;
        }
        
        if (comboCode == inputValues)
        {
            UnlockCombo();
        }
    }

    private void UnlockCombo()
    {
        isLocked = false;
        OnUnlock();

        lockedPanel.color = unlockedColor;
        lockedText.text = unlockedTextString;
        if (isResettable)
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        infoText.text = infoResetString;
        ResetUserValues();
        resetCombo = true;
    }

    private void LockCombo()
    {
        isLocked = true;
        OnLock();

        comboCode = inputValues;
        lockedPanel.color = lockedColor;
        lockedText.text = lockedTextString;

        infoText.text = infoStartString;

        ResetUserValues();
    }

    private void ResetUserValues()
    {
        inputValues = "";
        userInputText.text = inputValues;
        buttonPresses = 0;
    }
}
