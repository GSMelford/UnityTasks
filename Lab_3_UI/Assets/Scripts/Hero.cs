using System.Collections;
using System.Threading.Tasks;
using Movements;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private int MaxHp = 100;
    [SerializeField] private int MaxMana = 100;
    
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    [SerializeField] private string _attackAnumatorKey;
    [SerializeField] private string _hurtAnumatorKey;

    [Header("UI")]
    [SerializeField] private TMP_Text CoinValueText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider manaBar;
    
    private int _coinValue;
    private int _hp;
    private int _mana;
    private bool _attack;
    
    public int CoinValue
    {
        get => _coinValue;
        private set
        {
            _coinValue = value;
            CoinValueText.text = value.ToString();
        }
    }

    public int HP
    {
        get => _hp;
        private set
        {
            _hp = value;
            hpBar.value = value;
        }
    }
    
    public int Mana
    {
        get => _mana;
        private set
        {
            _mana = value;
            manaBar.value = value;
        }
    }
    
    
    private bool _jump;
    private float _direction;
    private bool _crawl;
        
    private MovementRigidbodyVelocity _movementRigidbodyVelocity;
    
    private void Start()
    {
        CoinValue = 0;
        HP = MaxHp;
        
        hpBar.maxValue = MaxHp;
        hpBar.value = MaxHp;
        Mana = MaxMana;
        manaBar.maxValue = MaxMana;
        manaBar.value = MaxMana;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _movementRigidbodyVelocity = new MovementRigidbodyVelocity(_rigidbody2D);
        
        Debug.Log(HP);
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
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(Mana);
            Mana -= 10;
        }
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

    public async void DoHurt(int damageValue)
    {
        //Auch
        _animator.SetBool(_hurtAnumatorKey, true);
        _spriteRenderer.color = Color.red;
        
        Debug.Log("I've taken damage!");

        HP -= damageValue;
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

    public void AddCoin(int value)
    {
        CoinValue += value;
        Debug.Log("Added coin");
        
    }

    public void HpUp(int hpBonus)
    {
        int missingHP = MaxHp - HP;
        int pointToAdd = missingHP > hpBonus ? hpBonus : missingHP;
        StartCoroutine(RestoreHp(pointToAdd));
    }

    private IEnumerator RestoreHp(int pointToAdd)
    {
        while (pointToAdd != 0)
        {
            HP++;
            pointToAdd--;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    private IEnumerator RestoreMana(int pointToAdd)
    {
        while (pointToAdd != 0)
        {
            Mana++;
            pointToAdd--;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AddMana(int manaBonus)
    {
        int missingMana = MaxMana - Mana;
        int pointToAdd = missingMana > manaBonus ? manaBonus : missingMana;
        StartCoroutine(RestoreMana(pointToAdd));
    }
}
