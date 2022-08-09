using UnityEngine;

public class BombActivation : MonoBehaviour
{
    [SerializeField] private GameObject _bombExplosion;

    private float _endScale = 1.5f;
    private bool _isActivation = false;

    private void Update()
    {
        if (_isActivation)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }
        if (transform.localScale.x >= _endScale)
        {
            Instantiate(_bombExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            _isActivation = true;
        }
    }
}
