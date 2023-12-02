using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour, IRotate
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _rotationSpeed = 180f;

    private CustomInput _input;
    private float _rotationInput;
    
    private void Awake()
    {
        _input = new CustomInput();
        _rotationInput = 0;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Rotate.performed += OnRotatePerformed;
        _input.Player.Rotate.canceled += OnRotateCanceled;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Rotate.performed -= OnRotatePerformed;
        _input.Player.Rotate.canceled -= OnRotateCanceled;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void OnRotatePerformed(InputAction.CallbackContext value)
    {
        _rotationInput = value.ReadValue<float>();
    }

    private void OnRotateCanceled(InputAction.CallbackContext value)
    {
        _rotationInput = 0;
    }

    public void Rotate()
    {
        _rb.transform.Rotate(-_rotationInput * _rotationSpeed * Time.deltaTime * transform.forward);
    }

    public void Initialize(Rigidbody2D rb)
    {
        _rb = rb;
    }
}
