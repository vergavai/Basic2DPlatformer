using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;

    private const int FacingRight = 1;
    private const int FacingLeft = -1;
    private const int NotMoving = 0;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private TouchingDirections _touchingDirections;
    private int _direction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);
    }

    private void Update()
    {
        Jump();
        MoveInDirection();

        FlipDirection();
        SetAnimatorParameters();
    }

    private void MoveInDirection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _direction = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = 1;
        }
        else if (_touchingDirections.IsGrounded)
        {
            _direction = 0;
        }
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimationStrings.IsGrounded, _touchingDirections.IsGrounded);
        _animator.SetBool(AnimationStrings.IsMoving, _direction != NotMoving);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _touchingDirections.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);

            _animator.SetTrigger(AnimationStrings.Jump);
        }
    }

    private void FlipDirection()
    {
        transform.eulerAngles = _direction switch
        {
            FacingRight => new Vector3(0, 0, 0),
            FacingLeft => new Vector3(0, 180f, 0),
            _ => transform.eulerAngles
        };
    }
}