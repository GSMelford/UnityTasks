using Movements;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _headCheckerRadius;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private Collider2D _headCollider;
    [SerializeField] private Transform _headChecker;
    
    private bool _jump;
    private float _direction;
    private bool _crawl;
        
    private MovementRigidbodyVelocity _movementRigidbodyVelocity;
    private MovementRigidbodyAddForce _movementRigidbodyAddForce;
    private MovementTransform _movementTransform;
    private MovementTransformTranslate _movementTransformTranslate;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_movementRigidbodyVelocity = new MovementRigidbodyVelocity(_rigidbody2D);
        //_movementRigidbodyAddForce = new MovementRigidbodyAddForce(_rigidbody2D);
        //_movementTransform = new MovementTransform(transform);
        //_movementTransformTranslate = new MovementTransformTranslate(transform);
    }
    
    private void Update()
    {
        CheckInputDirection();
        
        //1
        //_movementRigidbodyVelocity.DoMovement(_direction, _speed);
        //2
        //_movementRigidbodyAddForce.DoMovement(_direction, _speed);
        //3
        //_movementTransform.DoMovement(_direction, _speed);
        //4
        //_movementTransformTranslate.DoMovement(_direction, _speed);
        
        ChangeHeroDirection(_direction);
        CheckInput();
    }

    private void FixedUpdate()
    {
        DoJump();
        CheckHead();
    }

    private void CheckInputDirection()
    {
        _direction =  Input.GetAxisRaw("Horizontal");
    }
    
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }

        _crawl = Input.GetKey(KeyCode.C);
    }
    
    private void DoJump()
    {
        if (_jump && Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround))
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }
    }

    private void CheckHead()
    {
        if (_crawl)
        {
            _headCollider.enabled = false;
        }
        else if(!Physics2D.OverlapCircle(_headChecker.position, _headCheckerRadius, _whatIsGround))
        {
            _headCollider.enabled = true;
        }
    }
    
    private void ChangeHeroDirection(float direction)
    {
        if (direction > 0 && _spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
        else if (direction < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_headChecker.position, _headCheckerRadius);
    }
}
