using UnityEngine;

public class HookPickup : MonoBehaviour
{
    [SerializeField] private GameObject hook;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hook.SetActive(true);
            Destroy(gameObject);
        }
    }
}
