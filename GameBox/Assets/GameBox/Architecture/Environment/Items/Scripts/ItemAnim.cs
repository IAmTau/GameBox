using UnityEngine;

public class ItemAnim : MonoBehaviour
{
    private float _speedAnim = 0.35f;
    private float _changeSize = 0.2f;
    private float _startSize;
    private float _endSize;

    private void Start()
    {
        _startSize = transform.localScale.x;
        _endSize = _startSize + _startSize * _changeSize;
    }

    private void Update()
    {
        transform.localScale = new Vector2(Mathf.PingPong(Time.time * _speedAnim, _endSize - _startSize) + _startSize, 
                                           Mathf.PingPong(Time.time * _speedAnim, _endSize - _startSize) + _startSize);
    }
}
