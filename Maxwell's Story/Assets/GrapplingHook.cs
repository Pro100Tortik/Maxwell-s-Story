using System;
using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public event Action CanHook;

    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private Transform gunpoint, playerCamera;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float smooth = 25;
    [SerializeField] private float maxGrappleRange = 25.0f;
    [SerializeField] private FPSController controller;
    private Vector3 _grapplePoint;
    private Vector3 _currentGrapplePosition;
    private bool _isHooked;

    private void Awake()
    {
        _currentGrapplePosition = gunpoint.position;
        _grapplePoint = gunpoint.position;
        rope.enabled = false;
        rope.positionCount = 2;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        RaycastHit hitInfo;

        Physics.Raycast(ray, out hit, maxGrappleRange);

        if (Physics.SphereCast(ray, 0.5f,
            out hitInfo, hit.transform != null ? hit.distance : maxGrappleRange,
            grappleLayer, QueryTriggerInteraction.Ignore))
        {
            CanHook?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !_isHooked)
        {
            ThrowHook();
        }
    }

    private void LateUpdate() => DrawRope();

    private void ThrowHook()
    {
        rope.enabled = true;
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        RaycastHit hitInfo;

        Physics.Raycast(ray, out hit, maxGrappleRange);

        if (Physics.SphereCast(ray, 0.5f,
            out hitInfo, hit.transform != null ? hit.distance : maxGrappleRange,
            grappleLayer, QueryTriggerInteraction.Ignore))
        {
            _grapplePoint = hitInfo.point;
            controller.IsHooking = true;
            controller.CanMove = false;
            _isHooked = true;

            StartCoroutine(HookHit());
        }
        else
        {
            _isHooked = true;
            _grapplePoint = transform.position + transform.forward * maxGrappleRange;
            StartCoroutine(HookMiss());
        }
    }

    private void ReturnHook()
    {
        controller.IsHooking = false;
        controller.CanMove = true;

        _isHooked = false;
        _grapplePoint = gunpoint.position;
        _currentGrapplePosition = _grapplePoint;
        rope.enabled = false;
    }

    private void DrawRope()
    {
        if (_grapplePoint == gunpoint.position)
        {
            return;
        }

        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _grapplePoint, Time.deltaTime * smooth);

        rope.SetPosition(0, gunpoint.position);
        rope.SetPosition(1, _currentGrapplePosition);
    }

    private IEnumerator HookHit()
    {
        yield return new WaitForSeconds(0.3f);
        controller.MoveToHookPosition(Vector3.Normalize(_grapplePoint - playerCamera.position));
        while (Vector3.Distance(_grapplePoint, playerCamera.position) > 2)
        {
            yield return null;
        }
        ReturnHook();
        yield break;
    }

    private IEnumerator HookMiss()
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(0.15f);
        ReturnHook();
        yield break;
    }
}
