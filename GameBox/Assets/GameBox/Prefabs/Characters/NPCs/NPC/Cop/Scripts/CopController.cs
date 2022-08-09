using UnityEngine;

public class CopController : MonoBehaviour
{
    [SerializeField] private Transform _leftmostTransform;
    [SerializeField] private Transform _rightmostTransform;
    [SerializeField] private Transform _playerTransform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float _speed = 1f;
    private float _freezFactor = 1f;
    private bool _isSees;

    private float _idleDistanse = 1f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isSees)
        {
            float _distToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

            if (transform.position.x <= _leftmostTransform.position.x && transform.rotation.y == -1 || 
                transform.position.x >= _rightmostTransform.position.x && transform.rotation.y == 1 || 
                _distToPlayer < _idleDistanse)
            {
                _speed = 0;
                _animator.SetBool("Stay", true);

                if (_playerTransform.position.x > transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (_playerTransform.position.x < transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                StartHunting();
                _animator.SetBool("Stay", false);
            }          
        }
        else
        {
            _speed = 0;
            _animator.SetBool("Stay", true);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.left * _speed * _freezFactor;
    }

    private void StartHunting()
    {
        if (_playerTransform.position.x > transform.position.x)
        {
            _speed = -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (_playerTransform.position.x < transform.position.x)
        {
            _speed = 1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
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

    private void UnFreezing()
    {
        _freezFactor = 1;
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
