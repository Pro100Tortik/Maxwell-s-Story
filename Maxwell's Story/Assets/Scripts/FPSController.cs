using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [SerializeField] private float movementSpeed = 9.0f;
    [SerializeField] private float hookSpeed = 12.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float maxSlopeAngle = 50.0f;
    [SerializeField] private float maxAirSpeed = 1.0f;
    [SerializeField] private float friction = 4.0f;
    private Rigidbody _rigidbody;

    public bool IsHooking { get; set; }
    public bool CanMove { get; set; }
    public bool IsGrounded { get; private set; }

    public float GetVelocity() => _rigidbody.velocity.magnitude;

    private Vector3 _velocity;
    private Vector3 _input;
    private Vector3 _moveDirection;
    private Vector3 _groundNormal;
    private float _currentMovementSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        CanMove = true;

        _currentMovementSpeed = movementSpeed;
    }

    private void GetInputs()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float right = Input.GetAxisRaw("Horizontal");

        _input = new Vector3(right, 0, forward);
        _input.Normalize();

        _moveDirection = orientation.rotation * _input;

        _currentMovementSpeed = (Input.GetKey(KeyCode.LeftShift) ? 0.5f : 1.0f) * movementSpeed;
    }

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.velocity;

        BasicMovement();
        BasicPhysics();

        _rigidbody.velocity = _velocity;

        IsGrounded = false;
        _groundNormal = Vector3.zero;
    }

    public void MoveToHookPosition(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.velocity = position * hookSpeed;
    }

    private void BasicMovement()
    {
        if (!CanMove) return;

        if (IsGrounded)
        {
            _moveDirection = Vector3.Cross(Vector3.Cross(_groundNormal, _moveDirection), _groundNormal);
            GroundAcceleration();
        }
        else
        {
            AirAcceleration();
        }
    }

    private void BasicPhysics()
    {
        _rigidbody.drag = IsHooking ? 0 : IsGrounded ? friction : 0;
        _rigidbody.useGravity = (IsGrounded || IsHooking) ? false : true;
    }

    private void GroundAcceleration()
    {
        float currentspeed = Vector3.Dot(_velocity, _moveDirection);

        float addspeed = _currentMovementSpeed - currentspeed;

        if (addspeed <= 0)
            return;

        float accelspeed = acceleration * _currentMovementSpeed * Time.fixedDeltaTime;

        if (accelspeed > addspeed)
            accelspeed = addspeed;

        _velocity += _moveDirection * accelspeed;
    }

    private void AirAcceleration()
    {
        float wishspd = _currentMovementSpeed;

        if (wishspd > maxAirSpeed)
            wishspd = maxAirSpeed;

        float currentspeed = Vector3.Dot(_velocity, _moveDirection);

        float addspeed = wishspd - currentspeed;

        if (addspeed <= 0)
            return;

        float accelspeed = acceleration * _currentMovementSpeed * Time.fixedDeltaTime;

        if (accelspeed > addspeed)
            accelspeed = addspeed;

        _velocity += _moveDirection * accelspeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contactPoint in collision.contacts)
        {
            if (contactPoint.normal.y >= maxSlopeAngle * Mathf.Deg2Rad)
            {
                IsGrounded = true;
                _groundNormal = contactPoint.normal;
                return;
            }
        }
    }
}
