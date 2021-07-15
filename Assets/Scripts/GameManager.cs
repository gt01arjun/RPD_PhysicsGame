using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private GameObject _prepCamera;

    [SerializeField]
    private GameObject _gameCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var allPlatforms = FindObjectsOfType<Platform>();
            foreach (var platform in allPlatforms)
            {
                platform.GetComponent<BoxCollider>().enabled = true;
            }
            _ball.SetActive(true);
            _prepCamera.SetActive(false);
            _gameCamera.SetActive(true);
        }
    }
}