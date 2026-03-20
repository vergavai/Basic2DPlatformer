using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [SerializeField] private ContactFilter2D castFilter;

    private Collider2D _touchingCollider;
    private readonly float _groundDistance = 0.05f;

    private bool _isGrounded;

    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];

    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _touchingCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = _touchingCollider.Cast(Vector2.down, castFilter, _groundHits, _groundDistance) > 0;
    }
}