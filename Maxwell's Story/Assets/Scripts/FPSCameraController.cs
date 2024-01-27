using UnityEngine;
using DG.Tweening;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField] private SettingsSaver settings;
    [SerializeField] private Transform orientation, head;
    [SerializeField] private Camera playerCamera;
    private float _yaw, _pitch;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        _yaw += Input.GetAxisRaw("Mouse X") * settings.GameSettings.sensitivity;
        _pitch += -Input.GetAxisRaw("Mouse Y") * settings.GameSettings.sensitivity;

        _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
        orientation.localRotation = Quaternion.Euler(0, _yaw, 0);

        transform.position = head.position;
    }

    public void ChangeFov(float fov)
    {
        DOTween.To(() => playerCamera.fieldOfView, x => playerCamera.fieldOfView = x, fov, 0.2f);
    }
}
