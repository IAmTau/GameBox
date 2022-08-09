using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public static bool isAttack = false;

    #region privateFields
    // Components.
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    // Run.
    [SerializeField] private float _speedRunning = 1.3f;
    private float _directionMove = 0;
    private bool _movingRightInput;
    private bool _movingLeftInput;

    // Jump.
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.05f;
    [SerializeField] private int _extraJumpsValue = 2;
    [SerializeField] private float _jumpForse = 2.5f;
    private int _extraJumps;
    private bool _isGrounded;
    private bool _jumpInput;

    // Drop off platform.
    private bool _dropInput;


    // Attack.
    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private float _attackDelay = 0.1f;
    private float _timeAnimAttack = 0.5f;
    private bool _attackInput;

    // Hurt.
    [SerializeField] private AnimationClip _animationHurt;
    private float _timeAnimHurt = 0.4f;
    private int _startHealth;
    private bool _isHurt = false;

    // Freez.
    private float _freezFactor;
    #endregion  

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animationAttack != null) 
        {
            _timeAnimAttack = _animationAttack.length;
        }

        if (_animationHurt != null)
        {
            _timeAnimHurt = _animationHurt.length;
        }

        _extraJumps = _extraJumpsValue;

        _startHealth = UIController.countHealth;

        _freezFactor = 1;

        isAttack = false;
        _isHurt = false;
    }

    private void Update()
    {
        if (_animator != null && _rigidbody.simulated == true)
        {
            IsUserInput();
            IsUIController();

            if (!isAttack && !_isHurt)
            {
                ToRun();
                ToJump();
                ToDropOff();
                ToAttack();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_directionMove * _speedRunning * _freezFactor, _rigidbody.velocity.y);
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }
    private void IsUserInput()
    {
        _movingRightInput = Input.GetKey(KeyCode.D);
        _movingLeftInput = Input.GetKey(KeyCode.A);

        _jumpInput = Input.GetKeyDown(KeyCode.Space);

        _dropInput = Input.GetKeyDown(KeyCode.S);

        _attackInput = Input.GetMouseButtonDown(0);
    }

    private void IsUIController()
    {
        if (UIController.countHealth <= 0 || UIController.countHealth == 10)
        {
            _startHealth = 10;
        }
        else if (UIController.countHealth < _startHealth)
        {
            _startHealth = UIController.countHealth;
            _animator.SetTrigger("Damage");

            IsStopAnimation();

            _isHurt = true;
            Invoke("IsHurt", _timeAnimHurt);
        }
    }

    private void IsStopAnimation()
    {
        _animator.SetBool("Fall", false);
        _animator.SetBool("Run", false);
        _directionMove = 0;
    }

    private void ToRun()
    {
        if (_movingRightInput)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _directionMove = 1;
        }
        else if (_movingLeftInput)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            _directionMove = -1;
        }
        else
        {
            _directionMove = 0;
        }

        _animator.SetBool("Run", _movingRightInput || _movingLeftInput);
    }

    private void ToJump()
    {
        if (_isGrounded)
        {
            _extraJumps = _extraJumpsValue;
        }

        if (_jumpInput && _extraJumps > 0)
        {
            _rigidbody.velocity = Vector2.up * _jumpForse;
            _extraJumps--;
        }
        else if (_jumpInput && _extraJumps == 0 && _isGrounded)
        {
            _rigidbody.velocity = Vector2.up * _jumpForse;
        }

        _animator.SetBool("Fall", !_isGrounded);
    }

    private void ToDropOff()
    {
        if (_dropInput)
        {
            _animator.SetTrigger("DropOff");
        }
    }

    private void ToAttack()
    {
        if (_attackInput && !isAttack && _isGrounded)
        {
            _directionMove = 0;
            isAttack = true;
            _animator.SetTrigger("Attack");
            Invoke("IsAttack", _timeAnimAttack + _attackDelay);
        }
    }

    private void IsAttack()
    {
        isAttack = false;
    }

    private void IsHurt()
    {
        _isHurt = false;
        _animator.SetTrigger("AnimHurtFinish");
    }

    private void UnFreezing()
    {
        _freezFactor = 1;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FreezPlatform"))
        {
            _freezFactor = new Constant().freezingFactor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FreezPlatform"))
        {
            Invoke("UnFreezing", new Constant().freezingTime);
        }
    } 
}