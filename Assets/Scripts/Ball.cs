using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _inPocket;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            _inPocket = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            _inPocket = false;
        }
    }

    private void Update()
    {
        if (LevelManager.IsGameOver)
            return;

        if (_rb.velocity.magnitude <= 0 && _inPocket)
        {
            LevelManager.GameWin.Invoke();
        }

        if (_rb.velocity.magnitude <= 0.02f && _inPocket == false && LevelManager.RetriesLeftCounter <= 0)
        {
            LevelManager.GameLose.Invoke();
        }
    }
}