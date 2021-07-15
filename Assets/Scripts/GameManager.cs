using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private GameObject _prepCamera;

    [SerializeField]
    private GameObject _gameCamera;

    private Vector3 _ballInitialPos;

    private void Start()
    {
        _ballInitialPos = _ball.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _ball.SetActive(true);
            _prepCamera.SetActive(false);
            _gameCamera.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            _ball.transform.position = _ballInitialPos;
        }
    }
}