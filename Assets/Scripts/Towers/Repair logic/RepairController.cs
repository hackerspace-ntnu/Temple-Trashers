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

    private float lastWearStateChangeTime;

    public WearState CurrentWearState
    {
        get => _currentWearState;
        private set
        {
            _currentWearState = value;
            lastWearStateChangeTime = Time.time;
        }
    }

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

    public void Repair()
    {
        ResetWearState();
    }

    private void ResetWearState()
    {
        onWearStateChange?.Invoke(CurrentWearState, CurrentWearState);
        CurrentWearState = WearState.NONE;
    }

    private void NextState()
    {
        WearState prevState = CurrentWearState;

        switch (CurrentWearState)
        {
            case WearState.NONE:
                CurrentWearState = WearState.LOW;
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
            default:
                break;
        }

        onWearStateChange?.Invoke(CurrentWearState, prevState);
    }
}
