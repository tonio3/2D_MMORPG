using System;
using UnityEngine;

public abstract class CloudSaveInitializable : ScriptableObject
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        if (!IsInitialized)
        {
            OnInitialize();
            IsInitialized = true;
        }

        else
        {
           Debug.LogError("Already initialized.");               
        }
    }

    public void DeInitialize()
    {
        if (IsInitialized)
        {
            OnDeInitialize();
            IsInitialized = false;
        }

        else
        {
            Debug.LogError("Already Deinitialized.");
        }
    }

    protected void OnInitialize()
    {
        SubscribeToPlayerEvents();
    }
 
    protected void OnDeInitialize()
    {
        UnsubscribeFromPlayerEvents(); 
    }

    protected abstract void SubscribeToPlayerEvents();
    protected abstract void UnsubscribeFromPlayerEvents();

}