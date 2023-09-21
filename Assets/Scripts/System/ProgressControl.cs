using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System;

public class ProgressControl : MonoBehaviour
{
    public UnityEvent<string> OnStartGame;
    public UnityEvent<string> OnChallengeComplete;

    [Header("Start Button")]
    [SerializeField] private ButtonInteractable startButton = null;

    [Header("Key")]
    [SerializeField] private GameObject keyLight = null;
    [SerializeField] private MeshRenderer[] keyParts = null;
    [SerializeField] private Material keyEmissionMat = null;
    [SerializeField] private GameObject particles = null;

    [Header("Drawer")]
    [SerializeField] private DrawerInteractable drawer = null;
    XRSocketInteractor drawerSocket = null;

    [Header("Combo Lock")]
    [SerializeField] private CombinationLock comboLock = null;

    [Header("The Wall")]
    [SerializeField] private TheWall wall = null;
    XRSocketInteractor wallSocket = null;

    [Header("Building")]
    [SerializeField] SimpleSliderControl buildingSlider = null;

    [Header("Challenge Settings")]
    [SerializeField] private string startMessage = null;
    [SerializeField] private string[] challengeMessages = null;

    private bool startGame = false;
    private int challengeNumber = 0;

    private void Start()
    {
        startButton.selectEntered.AddListener(StartButtonPressed);
        OnStartGame?.Invoke(startMessage);
        SetDrawerInteractable();
        comboLock.UnlockAction += OnUnlockAction;
        SetWall();
        buildingSlider?.OnSliderActive.AddListener(BuildingSliderActive);
    }

    private void ChallengeComplete()
    {
        challengeNumber++;
        if (challengeNumber < challengeMessages.Length)
        {
            OnChallengeComplete?.Invoke(challengeMessages[challengeNumber]);
        }
        else if (challengeNumber >= challengeMessages.Length)
        {
            OnChallengeComplete?.Invoke("You Win!");
        }
    }

    private void StartButtonPressed(SelectEnterEventArgs args)
    {
        if (startGame) return;

        startGame = true;

        keyLight.SetActive(true);
        foreach (MeshRenderer keyPart in keyParts)
        {
            keyPart.material = keyEmissionMat;
        }
        particles.SetActive(true);
        OnStartGame?.Invoke(challengeMessages[challengeNumber]);

    }

    private void SetDrawerInteractable()
    {
        drawerSocket = drawer.GetKeySocket;
        drawerSocket.selectEntered.AddListener(OnDrawerSocketed);
        drawer.OnDrawerDetached.AddListener(OnDrawerDetach);
    }

    private void OnDrawerDetach()
    {
        ChallengeComplete();
    }

    private void OnDrawerSocketed(SelectEnterEventArgs args)
    {
        ChallengeComplete();
    }

    private void OnUnlockAction()
    {
        ChallengeComplete();
    }

    private void SetWall()
    {
        wall.OnDestroy.AddListener(OnWallDestroyed);
        wallSocket = wall.GetWallSocket;
        wallSocket?.selectEntered.AddListener(OnWallSocketed);
    }

    private void OnWallSocketed(SelectEnterEventArgs arg0)
    {
        ChallengeComplete();
    }

    private void OnWallDestroyed()
    {
        ChallengeComplete();
    }

    private void BuildingSliderActive()
    {
        ChallengeComplete();
    }
}
