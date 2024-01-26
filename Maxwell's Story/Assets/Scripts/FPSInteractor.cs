using UnityEngine;

public class FPSInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3.0f;
    [SerializeField] private float interactionCheckRadius = 0.2f;
    [SerializeField] private LayerMask interactableLayer;
    private Transform _playerCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interact();
        }
    }

    private void Interact()
    {
        RaycastHit hitInfo;
        RaycastHit hit;
        Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);

        Physics.Raycast(ray, out hit, interactionRange);

        if (Physics.SphereCast(ray, interactionCheckRadius,
            out hitInfo, hit.transform != null ? hit.distance : interactionRange,
            interactableLayer, QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable;
            if (hitInfo.collider.TryGetComponent<IInteractable>(out interactable))
            {
                interactable.Interact();
            }
        }
    }
}
