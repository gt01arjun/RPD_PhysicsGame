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

    private GameObject _currentPlatform;
    private GameObject _mainCamera;

    private Color _color = Color.red;

    private float _axisLockValue;

    private RaycastHit hit;

    private bool isDeleteMode;

    private void Start()
    {
        _canSpawn = true;
        _canPlace = false;
        _mainCamera = Camera.main.gameObject;
        isDeleteMode = false;
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
            CreateMode();
        }
        else
        {
            _deletePlatformHelperSphere.SetActive(true);
            DeleteMode();
        }
    }

    private bool CheckWall()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
        {
            _axisLockValue = hit.collider.GetComponent<Wall>().LockedAxisValue;
            return true;
        }
        return false;
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3();

        spawnPos = new Vector3(hit.point.x, hit.point.y, _axisLockValue);

        _currentPlatform = Instantiate(_platformPrefab, spawnPos, Quaternion.identity);
        _color.a = 0.4f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
    }

    private void PlacePlatform()
    {
        _color.a = 1f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
        _currentPlatform.GetComponent<Platform>().enabled = false;
        _currentPlatform.GetComponent<BoxCollider>().enabled = true;
    }

    private void CreateMode()
    {
        if (Input.GetMouseButtonDown(0) && _canSpawn)
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
            Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit);
            _currentPlatform.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
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