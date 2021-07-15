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

    private string _axisLockName;
    private float _axisLockValue;

    private RaycastHit hit;

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
            switch (_axisLockName)
            {
                case "X":
                    _currentPlatform.transform.position = new Vector3(_axisLockValue, hit.point.y, hit.point.z);
                    break;

                case "Y":
                    _currentPlatform.transform.position = new Vector3(hit.point.x, _axisLockValue, hit.point.z);
                    break;

                case "Z":
                    _currentPlatform.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
                    break;

                default:
                    break;
            }

        }

        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 100, Color.blue);
    }

    private bool CheckWall()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
        {
            _axisLockName = hit.collider.GetComponent<Wall>().LockedAxisName;
            _axisLockValue = hit.collider.GetComponent<Wall>().LockedAxisValue;
            return true;
        }
        return false;
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3();

        switch (_axisLockName)
        {
            case "X":
                spawnPos = new Vector3(_axisLockValue, hit.point.y, hit.point.z);
                break;

            case "Y":
                spawnPos = new Vector3(hit.point.x, _axisLockValue, hit.point.z);
                break;

            case "Z":
                spawnPos = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
                break;

            default:
                break;
        }

        _currentPlatform = Instantiate(_platformPrefab, spawnPos, Quaternion.identity);
        _color.a = 0.4f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
    }

    private void PlacePlatform()
    {
        _color.a = 1f;
        _currentPlatform.GetComponent<MeshRenderer>().material.color = _color;
        _currentPlatform.GetComponent<Platform>().enabled = false;
    }
}