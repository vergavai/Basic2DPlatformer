using UnityEngine;

public class PatrolingEnemy : MonoBehaviour
{
    [SerializeField] private float _patrolDistance;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Transform _patrolPoint;

    private Rigidbody2D _rigidbody;
    private int _direction = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_movingSpeed * _direction, _rigidbody.velocity.y);

        if (_direction == 1 && transform.position.x >= _patrolPoint.position.x + _patrolDistance)
        {
            Flip();
        }
        else if (_direction == -1 && transform.position.x <= _patrolPoint.position.x - _patrolDistance)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _direction *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}