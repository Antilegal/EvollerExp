using UnityEngine;
using UnityEngine.Events;

public class BaseEvent : MonoBehaviour
{
    private UnityEvent _onEvent = new UnityEvent();

    [HideInInspector] public UnityEvent onEvent => _onEvent;

    /// <summary>
    /// Execute event handler, invoke all listeners
    /// </summary>
    public virtual void OnHandle() => _onEvent.Invoke();

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        // Implementation in inherit classes
    }
}
