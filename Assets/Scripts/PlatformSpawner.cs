using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformPrefab;

    [SerializeField]
    private bool _canSpawn;
    [SerializeField]
    private bool _canPlace;

    [SerializeField]
    private GameObject _deletePlatformHelperSphere;

    [SerializeField]
    private GameObject[] _platformPrefabs;

    private GameObject _currentPlatform;
    private GameObject _mainCamera;

    private Color _color;

    private float _axisLockValue;

    private RaycastHit hit;

    private bool isDeleteMode;

    private int _currentScrollIndex;

    private void Start()
    {
        _canSpawn = true;
        _canPlace = false;
        _mainCamera = Camera.main.gameObject;
        isDeleteMode = false;
        _currentScrollIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isDeleteMode = !isDeleteMode;
        }

        if (isDeleteMode == false)
        {
            _deletePlatformHelperSphere.SetActive(false);
            if (_currentPlatform)
                _currentPlatform.SetActive(true);
            CreateMode();
        }
        else
        {
            _deletePlatformHelperSphere.SetActive(true);
            if (_currentPlatform)
                _currentPlatform.SetActive(false);
            DeleteMode();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            _currentScrollIndex++;
            if (_currentScrollIndex > _platformPrefabs.Length - 1)
            {
                _currentScrollIndex = 0;
            }
            _platformPrefab = _platformPrefabs[_currentScrollIndex];

            Destroy(_currentPlatform);
            SpawnPlatform();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            _currentScrollIndex--;
            if (_currentScrollIndex < 0)
            {
                _currentScrollIndex = _platformPrefabs.Length - 1;
            }
            _platformPrefab = _platformPrefabs[_currentScrollIndex];

            Destroy(_currentPlatform);
            SpawnPlatform();
        }
    }

    private bool CheckWall()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
        {
            if (hit.collider.GetComponent<Wall>())
            {
                _axisLockValue = hit.collider.GetComponent<Wall>().LockedAxisValue;
                return true;
            }
        }
        return false;
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3();

        spawnPos = new Vector3(hit.point.x, hit.point.y, _axisLockValue);

        _currentPlatform = Instantiate(_platformPrefab, spawnPos, Quaternion.identity);
        _color = _currentPlatform.GetComponent<MeshRenderer>().material.color;
        _color.a = 0.4f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
    }

    private void PlacePlatform()
    {
        _color.a = 1f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
        _currentPlatform.GetComponent<Platform>().enabled = false;
        _currentPlatform.GetComponent<Collider>().enabled = true;
    }

    private void CreateMode()
    {
        if (_canSpawn)
        {
            if (CheckWall())
            {
                SpawnPlatform();
                _canSpawn = false;
                _canPlace = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && _canPlace)
        {
            PlacePlatform();
            _canPlace = false;
            _canSpawn = true;
        }

        if (_canPlace)
        {
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
            {
                if (hit.collider.GetComponent<Wall>())
                {
                    _currentPlatform.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
                }
            }
        }

        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 100, Color.blue);
    }

    private void DeleteMode()
    {
        Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit);
        _deletePlatformHelperSphere.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 100, Color.red);
    }
}