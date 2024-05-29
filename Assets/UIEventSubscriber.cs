using UnityEngine;

public abstract class UIEventSubscriber : MonoBehaviour
{
    protected void OnEnable()
    {
        InitialUIUpdate();
        SubscribeToPlayerEvents();
    }

    protected void OnDisable()
    {
        UnsubscribeFromPlayerEvents();
    }

    protected abstract void InitialUIUpdate();
    protected abstract void SubscribeToPlayerEvents();
    protected abstract void UnsubscribeFromPlayerEvents();
}