using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WearState
{
    NONE,
    LOW,
    MEDIUM,
    HIGH,
}

public class RepairController : MonoBehaviour
{
    [SerializeField]
    private float wearInterval = 30f;

    [TextLabel(greyedOut = true), SerializeField]
    private WearState _currentWearState;

    private float lastWearStateChangeTime = default;

    [SerializeField]
    private AudioSource audioSource = default;

    public WearState CurrentWearState
    {
        get => _currentWearState;
        private set
        {
            _currentWearState = value;
            lastWearStateChangeTime = Time.time;
        }
    }

    [SerializeField]
    private TutorialText tutorialText = default;

    //To allow other components to subscribe to stateChange events
    public delegate void WearStateDelegate(WearState newState, WearState oldState);

    public WearStateDelegate onWearStateChange;

    void Start()
    {
        CurrentWearState = WearState.NONE;
    }

    void Update()
    {
        if (Time.time > lastWearStateChangeTime + wearInterval)
            NextState();
    }

    public void Explode()
    {
        Debug.Log("Object should probably use the RepairAnimationController instead if you want this function");
    }

    public void Repair()
    {
        audioSource.Play();
        ResetWearState();
    }

    private void ResetWearState()
    {
        WearState newState = WearState.NONE;
        onWearStateChange?.Invoke(newState, CurrentWearState);
        CurrentWearState = newState;
        tutorialText.SetButton(Direction.SOUTH, false);
    }

    private void NextState()
    {
        WearState prevState = CurrentWearState;

        switch (CurrentWearState)
        {
            case WearState.NONE:
                CurrentWearState = WearState.LOW;
                tutorialText.SetButton(Direction.SOUTH, true);
                break;
            case WearState.LOW:
                CurrentWearState = WearState.MEDIUM;
                break;
            case WearState.MEDIUM:
                CurrentWearState = WearState.HIGH;
                break;
            case WearState.HIGH:
                Destroy(gameObject);
                break;
        }

        onWearStateChange?.Invoke(CurrentWearState, prevState);
    }
}
