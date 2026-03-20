using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;

        private const int FacingRight = 1;
        private const int FacingLeft = -1;
        private const int NotMoving = 0;
        private const string HorizontalAxis = "Horizontal";

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private TouchingDirections _touchingDirections;
        private int _direction;
        private float _horizontalInput;
        private bool _jumped;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _touchingDirections = GetComponent<TouchingDirections>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);
            
            if (_jumped)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
                
                _animator.SetTrigger(AnimationStrings.Jump);
                _jumped = false;
            }
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw(HorizontalAxis);
            _jumped = Input.GetKey(KeyCode.Space) && _touchingDirections.IsGrounded;

            FlipDirection();
            SetAnimatorParameters();
        }

        private void SetAnimatorParameters()
        {
            _animator.SetBool(AnimationStrings.IsGrounded, _touchingDirections.IsGrounded);
            _animator.SetBool(AnimationStrings.IsMoving, _horizontalInput != NotMoving);
        }

        private void FlipDirection()
        {
            transform.localScale = _horizontalInput switch
            {
                FacingRight => new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z),
                FacingLeft => new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z),
                _ => transform.localScale
            };
        }
    }
}