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

    public static bool IsPrepMode;

    private void Start()
    {
        _ballInitialPos = _ball.transform.position;
        IsPrepMode = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && IsPrepMode)
        {
            _ball.GetComponent<Rigidbody>().useGravity = true;
            _prepCamera.SetActive(false);
            _gameCamera.SetActive(true);
            PlatformSpawner.CurrentPlatform.SetActive(false);
            IsPrepMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !IsPrepMode)
        {
            _ball.GetComponent<Rigidbody>().useGravity = false;
            _prepCamera.SetActive(true);
            _gameCamera.SetActive(false);
            PlatformSpawner.CurrentPlatform.SetActive(true);
            IsPrepMode = true;

            ResetBall();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _ball.transform.position = _ballInitialPos;
    }
}