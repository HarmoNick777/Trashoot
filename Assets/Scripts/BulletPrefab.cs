using UnityEngine;

public class BulletPrefab : MonoBehaviour, ICollide, IMoveForward
{
    [SerializeField] float _bulletLifeSpan = 1f;
    [SerializeField] float _bulletSpeed = 4f;
    [SerializeField] int _scoreWhenTrashDestroyed = 100;
    private Rigidbody2D _ship;
    private TrashSpawner _trashSpawner;
    private Rigidbody2D _rb;

    private void Awake()
    {
        Destroy(gameObject, _bulletLifeSpan);
        _trashSpawner = GameObject.FindObjectOfType<TrashSpawner>();
        _ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collide(collision);
    }

    private void IncrementScore()
    {
        TrashSpawner.score += _scoreWhenTrashDestroyed;
    }

    public void MoveForward()
    {
        Vector2 shipVelocity = _ship.velocity;
        Vector2 shipDirection = _ship.transform.up;
        float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

        _rb.velocity = shipDirection * shipForwardSpeed;

        _rb.AddForce(_bulletSpeed * shipDirection, ForceMode2D.Impulse);
    }

    public void Collide(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            _trashSpawner.trashCount--;
            IncrementScore();
        }
    }

    public void Initialize(TrashSpawner trashSpawner)
    {
        _trashSpawner = trashSpawner;
    }
}