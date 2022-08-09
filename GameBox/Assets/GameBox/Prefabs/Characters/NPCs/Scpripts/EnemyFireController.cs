using UnityEngine;

public class EnemyFireController : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _animEnemyExplosion;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AnimationClip _animationAttackPlayer;
    [SerializeField] private int _countHealth = 1;
    [SerializeField] private float _delayShoot = 2f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private int _amountExperience;

    private Color _colorStart;
    private Color _colorDamage = Color.red;

    private float _timeAnimAttackPlayer = 0.5f;
  
    private bool _isSees = false;
    private bool _delayShootPassed = true;
    private bool _isAnimAttackPlayerPlaying = false;

    private void Start()
    {
        if (_animationAttackPlayer != null)
        {
            _timeAnimAttackPlayer = _animationAttackPlayer.length;
        }
        _timeAnimAttackPlayer /= 2;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _colorStart = _spriteRenderer.color;

        _amountExperience = _countHealth * 10;
    }

    private void Update()
    {
        if (_isSees && _delayShootPassed)
        {
            GetComponent<AudioSource>().Play();

            _animator.SetTrigger("Shoot");

            Instantiate(_projectile, _firePoint.position, _firePoint.rotation);

            _delayShootPassed = false;
            Invoke("ToShoot", _delayShoot);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BombExplosion"))
        {
            _countHealth -= new Constant().damageBomb;
            GetDamage();

            if (gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerConroller.isAttack && !_isAnimAttackPlayerPlaying)
        {
            _isAnimAttackPlayerPlaying = true;
            _countHealth -= new Constant().attackPlayerDamage;
            _spriteRenderer.color = _colorDamage;
            Invoke("GetDamage", _timeAnimAttackPlayer);
        }

        if (!PlayerConroller.isAttack)
        {
            _isAnimAttackPlayerPlaying = false;
        }
    }

    private void ToShoot()
    {
        _delayShootPassed = true;
    }

    private void GetDamage()
    {
        _spriteRenderer.color = _colorStart;
        if (_countHealth <= 0)
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