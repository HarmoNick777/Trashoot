using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveForward : MonoBehaviour, IMoveForward
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _acceleration = 5f;
    [SerializeField] float _maxVelocity = 5f;
    [SerializeField] float _deceleration = 1f;

    private CustomInput _input;
    private bool _isAccelerating;
    
    private void Awake()
    {
        _input = new CustomInput();
        _isAccelerating = false;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.MoveForward.performed += OnMoveForwardPerformed;
        _input.Player.MoveForward.canceled += OnMoveForwardCanceled;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.MoveForward.performed -= OnMoveForwardPerformed;
        _input.Player.MoveForward.canceled -= OnMoveForwardCanceled;
    }

    private void FixedUpdate()
    {
        if (_isAccelerating)
        {
            MoveForward();
        }
        else
        {
            Decelerate();
        }
    }

    private void OnMoveForwardPerformed(InputAction.CallbackContext value)
    {
        _isAccelerating = true;
    }

    private void OnMoveForwardCanceled(InputAction.CallbackContext value)
    {
        _isAccelerating = false;
    }

    public void MoveForward()
    {
        _rb.AddForce(_acceleration * _rb.transform.up);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxVelocity);
    }

    private void Decelerate()
    {
        _rb.velocity *= _deceleration;
    }

    public void Initialize(Rigidbody2D rb)
    {
        _rb = rb;
    }
}