using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashPrefab : MonoBehaviour, ICollide, IMoveForward
{
    [SerializeField] float _minSpeed = 4f;
    [SerializeField] float _maxSpeed = 5f;
    [SerializeField] GameObject _ship;
    
    private void Start()
    {
        MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collide(collision);
    }

    public void MoveForward()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(_ship.transform.position.x - rb.position.x, _ship.transform.position.y - rb.position.y).normalized;
        float speed = Random.Range(_minSpeed, _maxSpeed);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void Collide(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Those two Destroy are unnecessary for the gameplay but required for the unit test of the collision between the player and a trash
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
    }
}
