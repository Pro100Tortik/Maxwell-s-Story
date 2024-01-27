using UnityEngine;

public class FPSInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3.0f;
    [SerializeField] private float interactionCheckRadius = 0.2f;
    [SerializeField] private LayerMask interactableLayer;
    private Transform _playerCamera;

    public bool CanInteract { get; private set; }

    private void Awake()
    {
        _playerCamera = transform;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        RaycastHit hitInfo;
        RaycastHit hit;
        Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);

        Physics.Raycast(ray, out hit, interactionRange);

        IInteractable interactable;
        Physics.SphereCast(ray, interactionCheckRadius,
            out hitInfo, hit.transform != null ? hit.distance : interactionRange,
            interactableLayer, QueryTriggerInteraction.Ignore);
        if (hitInfo.collider != null)
            CanInteract = hitInfo.collider.TryGetComponent<IInteractable>(out interactable);
        else
            CanInteract = false;

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
