using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private Transform orientation, head;
    private float _yaw, _pitch;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        _yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        _pitch += -Input.GetAxisRaw("Mouse Y") * sensitivity;

        _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
        orientation.localRotation = Quaternion.Euler(0, _yaw, 0);

        transform.position = head.position;
    }
}
