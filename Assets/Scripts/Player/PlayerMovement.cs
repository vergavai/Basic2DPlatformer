using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private TouchingDirections _touchingDirections;
    private Vector2 _moveInput;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput.x * _speed, _rigidbody.velocity.y);
    }

    private void Update()
    {
        Jump();
        MoveInDirection();
        
        ChangeDirection();
        SetAnimatorParameters();
    }

    private void MoveInDirection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _moveInput.x = -1;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveInput.x = 1;
        }
        else if(_touchingDirections.IsGrounded)
        {
            _moveInput.x = 0;
        }
        
    }

    private void SetAnimatorParameters()
    {
        _animator.SetBool(AnimationStrings.IsGrounded, _touchingDirections.IsGrounded);
        _animator.SetBool(AnimationStrings.IsMoving, _moveInput != Vector2.zero);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _touchingDirections.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
            
            _animator.SetTrigger(AnimationStrings.Jump);
        }
    }
    
    private void ChangeDirection()
    {
        if (_rigidbody.velocity.x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0); 
        else if (_rigidbody.velocity.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0); 
    }
    
}