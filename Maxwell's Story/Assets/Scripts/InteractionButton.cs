using UnityEngine;
using UnityEngine.Events;

public class InteractionButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent OnPress;
    
    public void Interact()
    {
        OnPress.Invoke();
        Destroy(this);
    }
}
