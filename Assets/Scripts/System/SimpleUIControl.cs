using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class SimpleUIControl : MonoBehaviour
{
    [SerializeField] private ProgressControl progressControl = null;
    [SerializeField] private TMP_Text[] messageTexts = null;

    private void OnEnable()
    {
        progressControl.OnStartGame.AddListener(StartGame);
        progressControl.OnChallengeComplete.AddListener(ChallengeComplete);
    }

    public void StartGame(string arg)
    {
        SetText(arg);
    }

    public void ChallengeComplete(string arg)
    {
        SetText(arg);
    }

    public void SetText(string text)
    {
        for (int i = 0; i < messageTexts.Length; i++)
        {
            messageTexts[i].text = text;
        }
    }
}
