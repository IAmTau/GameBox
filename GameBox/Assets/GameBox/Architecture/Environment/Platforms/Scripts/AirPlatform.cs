using UnityEngine;

public class AirPlatform : MonoBehaviour
{
    private PlatformEffector2D _effector;

    private void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _effector.rotationalOffset = 180;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _effector.rotationalOffset = 0;
        }
    }
}
