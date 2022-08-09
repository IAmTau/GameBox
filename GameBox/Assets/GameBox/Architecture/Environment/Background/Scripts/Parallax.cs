using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _folloingTarget;
    [SerializeField, Range(0f, 1f)] private float _patallaxStrength = 0.5f;
    private Vector3 _targetPreviousPosition;

    private void Start()
    {
        if (!_folloingTarget)
        {
            _folloingTarget = Camera.main.transform;
        }

        _targetPreviousPosition = _folloingTarget.position;
    }

    private void Update()
    {
        Vector3 _delta = _folloingTarget.position - _targetPreviousPosition;

        _delta.y = 0;

        _targetPreviousPosition = _folloingTarget.position;

        transform.position += _delta * _patallaxStrength;
    }
}
