using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] _checkpoints;
    private Animator _animator;
    private Vector3 _lastCheckpointPosition;
    private int _numberOfLives;
    private float _timeDeath = 1.5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _numberOfLives = new Constant().numberLives;
        _lastCheckpointPosition = transform.position;
    }

    private void Update()
    {
        if (UIController.countHealth <= 0)
        {
            _animator.SetBool("Fall", false);
            _animator.SetBool("Run", false);
            _animator.SetTrigger("Death");
            gameObject.GetComponent<Rigidbody2D>().simulated = false;

            Invoke("ToDeath", _timeDeath);

            UIController.countHealth = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint")) 
        { 
            _lastCheckpointPosition = collision.transform.position; 
        }

        if (collision.CompareTag("Abyss")) 
        {
            ToDeath();
        }
    }

    private void ToDeath() 
    {
        if (_numberOfLives <= 0)
        {
            _numberOfLives = new Constant().numberLives;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            _numberOfLives--;
            transform.position = _lastCheckpointPosition;
            UIController.countHealth = new Constant().startNumberOfHealth;
        }

        for (int i = 0; i < _checkpoints.Length; i++)
        {
            if (_checkpoints[i] != null)
            {
                _checkpoints[i].GetComponent<Animator>().SetTrigger("Death");
            }
        }

        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        _animator.SetTrigger("Resurrection");
    }
}
