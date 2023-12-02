using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour, IShoot
{
    [SerializeField] Transform _bulletSpawn;
    [SerializeField] Rigidbody2D _bulletPrefab;

    private CustomInput _input;

    private void Awake()
    {
        _input = new CustomInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Shoot.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Shoot.performed -= OnShootPerformed;
    }

    private void OnShootPerformed(InputAction.CallbackContext value)
    {
        Shoot();
    }

    public void Shoot()
    {
        Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.identity);
    }
}
