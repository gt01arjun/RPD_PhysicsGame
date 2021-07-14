using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformPrefab;

    [SerializeField]
    private bool _canSpawn;
    [SerializeField]
    private bool _canPlace;

    private GameObject _currentPlatform;
    private GameObject _mainCamera;

    private Color _color = Color.red;

    private void Start()
    {
        _canSpawn = true;
        _canPlace = false;
        _mainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canSpawn)
        {
            SpawnPlatform();
            _canSpawn = false;
            _canPlace = true;
        }

        else if (Input.GetMouseButtonDown(0) && _canPlace)
        {
            PlacePlatform();
            _canPlace = false;
            _canSpawn = true;
        }
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = _mainCamera.transform.position + _mainCamera.transform.forward * 10 + _mainCamera.transform.up * -2;
        _currentPlatform = Instantiate(_platformPrefab, spawnPos, Quaternion.identity);
        _color.a = 0.4f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
        _currentPlatform.transform.SetParent(_mainCamera.transform);
        _currentPlatform.transform.localRotation = Quaternion.identity;
    }

    private void PlacePlatform()
    {
        _color.a = 1f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
        _currentPlatform.GetComponent<Platform>().enabled = false;
        _currentPlatform.transform.parent = null;
    }
}