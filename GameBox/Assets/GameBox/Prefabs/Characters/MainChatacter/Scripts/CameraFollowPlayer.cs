using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    private float _transformZ = -1.73f;

    private void LateUpdate()
    {
        transform.position = new Vector3(_transformPlayer.position.x, 0.5f, _transformZ);
    }
}
