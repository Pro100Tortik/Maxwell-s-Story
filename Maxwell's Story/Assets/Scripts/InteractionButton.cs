using UnityEngine;
using UnityEngine.Events;

public class InteractionButton : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOneUse = false;
    [SerializeField] private UnityEvent OnPress;
    
    public void Interact()
    {
        OnPress.Invoke();
        if (isOneUse)
        {
            Destroy(this);
        }
    }
}
