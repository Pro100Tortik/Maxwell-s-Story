using UnityEngine;
using UnityEngine.Events;

public class TriggetZone : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private bool oneUse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter?.Invoke();

            if (oneUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
