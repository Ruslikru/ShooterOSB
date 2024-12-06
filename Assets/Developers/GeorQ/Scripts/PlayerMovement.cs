using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class PlayerMovement
{
    //Player's Components
    private Rigidbody _playerRb;
    private Transform _orientation;
    private LayerMask _maskToAvoid;
    private Transform _transform;
    //

    //Main flags
    private bool _isGrounded;
    private bool _isSlopeMovement;
    //

    private float _height = 2f;
    private float _heightOffset = 0.35f;
    private Vector3 _moveDir;
    private float _moveSepeed = 50f;
    private float _speedMultiplInAir = 0.02f;
    private float _jumpForce = 3f;

    ////CustomGravity
    //private float gravityValue = 0f;
    //private float downForce = 600f;
    ////


    public PlayerMovement(Rigidbody playerRb, Transform orientation, LayerMask maskToAvoid)
    {
        _playerRb = playerRb;
        _orientation = orientation;
        _maskToAvoid = maskToAvoid;
        _transform = _playerRb.transform;
    }

    public void Tick()
    {
        PlayerInput();
        IsSlopeMovement();
        IsGrounded();
        SetDrag();
        Jump();
    }

    public void PhysicsTick()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isSlopeMovement)
        {
            _playerRb.AddForce(_moveDir * _moveSepeed, ForceMode.Acceleration);
        }
        else if (_isGrounded)
        {
            _playerRb.AddForce(_moveDir * _moveSepeed, ForceMode.Acceleration);
        }
        else
        {
            _playerRb.AddForce(_moveDir * _moveSepeed * _speedMultiplInAir, ForceMode.Acceleration);
        }
    }

    private void IsSlopeMovement()
    {
        if (Physics.Raycast(_playerRb.transform.position, Vector3.down, out RaycastHit hit, 1.5f))
        {
            if (Vector3.Angle(hit.normal, Vector3.up) > 15)
            {
                _isSlopeMovement = true;
                _moveDir = Vector3.ProjectOnPlane(_moveDir, hit.normal).normalized;
                return;
            }
            _isSlopeMovement = false;
        }
    }

    private void PlayerInput()
    {
        _moveDir = _orientation.forward * Input.GetAxisRaw("Vertical") + _orientation.right * Input.GetAxisRaw("Horizontal");
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void IsGrounded()
    {
        Collider[] result = Physics.OverlapSphere(new Vector3(_transform.position.x, _transform.position.y - _height / 2 + _heightOffset, _playerRb.transform.position.z), 0.45f, ~_maskToAvoid);
        _isGrounded = result.Length > 0;
    }

    void SetDrag()
    {
        _playerRb.drag = _isGrounded ? (float) DragType.NormalDrag : (float)DragType.AirDrag;
    }

    //private void CustomGravity()
    //{
    //    if (_isGrounded)
    //    {
    //        gravityValue = 0;
    //        return;
    //    }

    //    gravityValue += downForce * Time.deltaTime * Time.deltaTime;
    //    _playerRb.AddForce(Vector3.down * gravityValue);
    //}
}