using UnityEngine;

public class BringerConroller : MonoBehaviour
{
    [SerializeField] private Transform _leftmostTransform;
    [SerializeField] private Transform _rightmostTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cenerBringerTransform;
    [SerializeField] private AnimationClip _attackBringerAnim;
    [SerializeField] private AnimationClip _attackPlayerAnim;
    [SerializeField] private GameObject _animEnemyExplosion;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float _speedRuning = 1f;
    private float _speedFactor = 1f;
    private int _amountOfHealth = 5;

    private float _delayAttack = 2f;
    private float _attackDistnse = 1f;
    private int _damageAttack = 2;

    private bool _isSees = false;
    private float _timeAcrivetionDamage = 1f;
    private bool _delayAttackPassed = true;

    private bool _canCausedDamage = false;

    private float _timeAnimAttackPlayer = 0.5f;
    private bool _isAnimAttackPlayerPlaying = false;
    private int _amountExperience;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (_attackPlayerAnim != null)
        {
            _timeAnimAttackPlayer = _attackPlayerAnim.length;
        }
        _timeAnimAttackPlayer /= 2;

        if (_attackBringerAnim != null)
        {
            _timeAcrivetionDamage = _attackBringerAnim.length;
        }

        _amountExperience = _amountOfHealth * 10;
    }

    private void Update()
    {
        if (_isSees && _delayAttackPassed)
        {
            ToMove();
        }
        else
        {
            _speedFactor = 0;
            _animator.SetBool("Stay", true);
        }

        if (Vector2.Distance(_cenerBringerTransform.position, _playerTransform.position) <= _attackDistnse && _canCausedDamage)
        {
            _canCausedDamage = false;
            UIController.countHealth -= _damageAttack;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.left * _speedRuning * _speedFactor;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerConroller.isAttack && !_isAnimAttackPlayerPlaying)
        {
            _isAnimAttackPlayerPlaying = true;
            _amountOfHealth -= new Constant().attackPlayerDamage;
            _animator.SetTrigger("Hurt");
            Invoke("GetDamage", _timeAnimAttackPlayer);
        }

        if (!PlayerConroller.isAttack)
        {
            _isAnimAttackPlayerPlaying = false;
        }
    }

    private void ToMove()
    {
        float _distToPlayer = Vector2.Distance(_cenerBringerTransform.position, _playerTransform.position);

        if (_cenerBringerTransform.position.x <= _leftmostTransform.position.x && transform.rotation.y == -1 ||
            _cenerBringerTransform.position.x >= _rightmostTransform.position.x && transform.rotation.y == 1 ||
            _distToPlayer <= _attackDistnse)
        {
            _speedFactor = 0;
            _animator.SetBool("Stay", true);

            if (_playerTransform.position.x > _cenerBringerTransform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (_playerTransform.position.x < _cenerBringerTransform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            CheckAttack(_distToPlayer);
        }
        else
        {
            StartHunting();
            _animator.SetBool("Stay", false);
        }
    }

    private void CheckAttack(float _distToPlayer)
    {
        if (_distToPlayer < _attackDistnse && _delayAttackPassed)
        {
            _speedFactor = 0;
            _delayAttackPassed = false;
            _animator.SetTrigger("Attack");
            Invoke("ToAttack", _delayAttack);
            Invoke("CanCausedDamage", _timeAcrivetionDamage / 2);
            Invoke("CantCausedDamage", _timeAcrivetionDamage / 1.5f);
        }
    }

    private void StartHunting()
    {
        if (_playerTransform.position.x > _cenerBringerTransform.position.x)
        {
            _speedFactor = -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (_playerTransform.position.x < _cenerBringerTransform.position.x)
        {
            _speedFactor = 1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ToAttack()
    {
        StartHunting();
        _canCausedDamage = false;
        _delayAttackPassed = true;
    }

    private void CanCausedDamage()
    {
        _canCausedDamage = true;
    }

    private void CantCausedDamage()
    {
        _canCausedDamage = false;
    }

    private void GetDamage()
    {
        if (_amountOfHealth <= 0)
        {
            ExperienceController.counterXP += _amountExperience;
            Instantiate(_animEnemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        _isSees = true;
    }

    private void OnBecameInvisible()
    {
        _isSees = false;
    }
}
