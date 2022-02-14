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
    private float damageInterval;

    [ReadOnly, SerializeField]
    private WearState _currentState;

    private float timeStamp;
    private bool needRepair = false;

    public WearState currentState { get => _currentState; private set => _currentState = value; }

    //To allow other components to subscribe to stateChange events
    public delegate void WearStateDelegate(WearState newState, WearState oldState);
    public WearStateDelegate onWearStateChange;

    void Start()
    {
        currentState = WearState.NONE;
        timeStamp = Time.time;
    }

    void Update()
    {
        if (Time.time > timeStamp + damageInterval)
        {
            timeStamp = Time.time;
            needRepair = true;
            NextState();
        }
    }

    public void Repair(){
        needRepair = false;
        timeStamp = Time.time;
        SetStateNONE();
    }

    private void SetStateNONE()
    {
        onWearStateChange?.Invoke(currentState, currentState);
        currentState = WearState.NONE;
    }

    private void NextState()
    {
        WearState prevState = currentState;

        switch (currentState)
        {
            case WearState.NONE:
                currentState = WearState.LOW;
                break;
            case WearState.LOW:
                currentState = WearState.MEDIUM;
                break;
            case WearState.MEDIUM:
                currentState = WearState.HIGH;
                break;
            case WearState.HIGH:
                Destroy(gameObject);
                break;
            default:
                break;
        }
        onWearStateChange?.Invoke(currentState, prevState);
    }
}
