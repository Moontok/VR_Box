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

    [Header("The Robot")]
    [SerializeField] NavMeshRobot robot = null;

    [Header("Building")]
    [SerializeField] SimpleSliderControl buildingSlider = null;

    [Header("Challenge Settings")]
    [SerializeField] private string startMessage = null;
    [SerializeField] private string endGameMessageString = null;
    [SerializeField] private string[] challengeMessages = null;
    [SerializeField] private int wallCubesToDestroy = 10;
    [SerializeField] private int challengeNumber = 0;

    private int wallCubesDestroyed = 0;
    private bool startGame = false;
    private bool challengesComplete = false;
    

    private void Start()
    {
        startButton.selectEntered.AddListener(StartButtonPressed);
        OnStartGame?.Invoke(startMessage);
        SetDrawerInteractable();
        comboLock.UnlockAction += OnUnlockAction;
        SetWall();
        buildingSlider?.OnSliderActive.AddListener(BuildingSliderActive);
        robot?.OnDestroyWallCube.AddListener(OnDestroyWallCube);
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
            OnChallengeComplete?.Invoke(endGameMessageString);
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
        if (challengeNumber == 0)
        {
            OnStartGame?.Invoke(challengeMessages[challengeNumber]);
        }
    }

    private void OnDrawerSocketed(SelectEnterEventArgs args)
    {
        if (challengeNumber == 0)
        {
            ChallengeComplete();
        }
    }

    private void OnDrawerDetach()
    {
        if (challengeNumber == 1)
        {
            ChallengeComplete();
        }
    }

    private void OnUnlockAction()
    {
        if (challengeNumber == 2)
        {
            ChallengeComplete();
        }
    }

    private void OnWallSocketed(SelectEnterEventArgs arg0)
    {
        if (challengeNumber == 3)
        {
            ChallengeComplete();
        }
    }

    private void OnWallDestroyed()
    {
        if (challengeNumber == 4)
        {
            ChallengeComplete();
        }
    }

    private void BuildingSliderActive()
    {
        if (challengeNumber == 5)
        {
            ChallengeComplete();
        }
    }

    private void OnDestroyWallCube()
    {
        wallCubesDestroyed++;
        if (wallCubesDestroyed >= wallCubesToDestroy && !challengesComplete)
        {
            challengesComplete = true;
            if (challengeNumber == 6)
            {
                ChallengeComplete();
            }
        }
    }

    private void SetDrawerInteractable()
    {
        drawerSocket = drawer.GetKeySocket;
        drawerSocket.selectEntered.AddListener(OnDrawerSocketed);
        drawer.OnDrawerDetached.AddListener(OnDrawerDetach);
    }

    private void SetWall()
    {
        wall.OnDestroy.AddListener(OnWallDestroyed);
        wallSocket = wall.GetWallSocket;
        wallSocket?.selectEntered.AddListener(OnWallSocketed);
    }
}
