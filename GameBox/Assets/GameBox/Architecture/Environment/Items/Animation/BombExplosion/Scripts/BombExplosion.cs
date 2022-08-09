using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private AnimationClip _animBombExplosion;
    private float _timeAnimBombExplosion = 0.9f;

    private void Start()
    {
        if (_animBombExplosion != null)
        {
            _timeAnimBombExplosion = _animBombExplosion.length;
        }

        GetComponent<AudioSource>().Play();

        Invoke("ToDestroy", _timeAnimBombExplosion);
    }

    private void ToDestroy()
    {
        Destroy(gameObject);
    }
}
