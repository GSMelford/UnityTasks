using System.Threading.Tasks;
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

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    [SerializeField] private string _attackAnumatorKey;
    [SerializeField] private string _hurtAnumatorKey;
    
    private bool _jump;
    private float _direction;
    private bool _crawl;
        
    private MovementRigidbodyVelocity _movementRigidbodyVelocity;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _movementRigidbodyVelocity = new MovementRigidbodyVelocity(_rigidbody2D);
    }
    
    private void Update()
    {
        CheckInputDirection();

        _animator.SetFloat(_runAnimatorKey, Mathf.Abs(_direction));
        _movementRigidbodyVelocity.DoMovement(_direction, _speed);
        
        
        ChangeHeroDirection(_direction);
        CheckInput();
    }

    private void FixedUpdate()
    {
        DoJump();
        CheckHead();
        _animator.SetBool(_crouchAnimatorKey, !_headCollider.enabled);
    }

    private void CheckInputDirection()
    {
        _direction =  Input.GetAxisRaw("Horizontal");
    }
    
    private void CheckInput()
    {
        _jump = Input.GetKey(KeyCode.Space);
        _animator.SetBool(_attackAnumatorKey, Input.GetKey(KeyCode.F));
        _crawl = Input.GetKey(KeyCode.C);
    }
    
    private void DoJump()
    {
        bool canJump = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround);
        if (_jump && canJump)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            
            _jump = false;
        }
        
        _animator.SetBool(_jumpAnimatorKey, !canJump);
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

    public async void DoHurt()
    {
        //Auch
        _animator.SetBool(_hurtAnumatorKey, true);
        _spriteRenderer.color = Color.red;
        
        Debug.Log("I've taken damage!");
        
        await Task.Delay(400);
        _spriteRenderer.color = Color.white;
        _animator.SetBool(_hurtAnumatorKey, false);
    }

    public void InteractionWithTurret()
    {
        Debug.Log("It spun for a long time ...");
    }
    
    public void InteractionWithMagicBall()
    {
        Debug.Log("Ball?!1");
    }
}
