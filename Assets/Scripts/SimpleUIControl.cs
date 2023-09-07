using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class SimpleUIControl : MonoBehaviour
{
    [SerializeField] private ButtonInteractable startButton = null;
    [SerializeField] private string[] messages = null;
    [SerializeField] private TMP_Text[] messageTexts = null;
    [SerializeField] private GameObject keyLight = null;
    [SerializeField] private MeshRenderer[] keyParts = null;
    [SerializeField] private Material keyEmissionMat = null;
    [SerializeField] private GameObject particles = null;

    private void Start()
    {
        if (startButton != null)
        {
            startButton.selectEntered.AddListener(StartButtonPressed);
        }
    }

    private void StartButtonPressed(SelectEnterEventArgs args)
    {
        SetText(messages[1]);
        keyLight.SetActive(true);
        foreach (MeshRenderer keyPart in keyParts)
        {
            keyPart.material = keyEmissionMat;
        }
        particles.SetActive(true);

    }

    public void SetText(string text)
    {
        for (int i = 0; i < messageTexts.Length; i++)
        {
            messageTexts[i].text = text;
        }
    }

}
