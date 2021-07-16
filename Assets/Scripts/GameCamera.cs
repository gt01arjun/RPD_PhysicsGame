using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float _smoothness;

    [SerializeField]
    private Transform _ball;
    
    [SerializeField]
    private Vector3 _initialOffset;

    private Vector3 _cameraPos;

    private void Update()
    {
        _cameraPos = _ball.position - _initialOffset;

        transform.position = Vector3.Lerp(transform.position, _cameraPos, _smoothness * Time.deltaTime);
    }
}