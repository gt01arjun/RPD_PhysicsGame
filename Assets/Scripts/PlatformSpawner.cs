using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private List<GameObject> _platformPrefabs;

    [SerializeField]
    private Material _woodOpaque;

    public static GameObject CurrentPlatform;
    private GameObject _mainCamera;

    [SerializeField]
    private float _axisLockValue;

    private RaycastHit hit;

    private bool isDeleteMode;

    private int _currentScrollIndex;

    public static UnityEvent<string> AddToPrefabList = new UnityEvent<string>();

    [SerializeField]
    private List<GameObject> _backupPlatforms;

    private void OnEnable()
    {
        AddToPrefabList.AddListener(AddToList);
    }

    private void AddToList(string tagName)
    {
        foreach (var plank in _backupPlatforms)
        {
            if (plank.CompareTag(tagName))
            {
                _platformPrefabs.Add(plank);
            }
        }
    }

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
        if (LevelManager.IsPrepMode == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && _platformPrefabs.Count > 0)
        {
            isDeleteMode = !isDeleteMode;
        }

        if (isDeleteMode == false)
        {
            _deletePlatformHelperSphere.SetActive(false);
            if (CurrentPlatform)
                CurrentPlatform.SetActive(true);
            if (_platformPrefab)
                CreateMode();
        }
        else
        {
            _deletePlatformHelperSphere.SetActive(true);
            if (CurrentPlatform)
                CurrentPlatform.SetActive(false);
            DeleteMode();
        }

        if (isDeleteMode)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && _platformPrefabs.Count > 0) // forward
        {
            _currentScrollIndex++;
            if (_currentScrollIndex > _platformPrefabs.Count - 1)
            {
                _currentScrollIndex = 0;
            }
            _platformPrefab = _platformPrefabs[_currentScrollIndex];

            Destroy(CurrentPlatform);
            SpawnPlatform();
            _canSpawn = false;
            _canPlace = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && _platformPrefabs.Count > 0) // backwards
        {
            _currentScrollIndex--;
            if (_currentScrollIndex < 0)
            {
                _currentScrollIndex = _platformPrefabs.Count - 1;
            }
            _platformPrefab = _platformPrefabs[_currentScrollIndex];

            Destroy(CurrentPlatform);
            SpawnPlatform();
            _canSpawn = false;
            _canPlace = true;
        }
    }

    private bool CheckWall()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
        {
            if (hit.collider.GetComponent<Wall>() || hit.collider.GetComponent<Platform>())
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPos = new Vector3();

        spawnPos = new Vector3(hit.point.x, hit.point.y, _axisLockValue);

        CurrentPlatform = Instantiate(_platformPrefab, spawnPos, Quaternion.identity);
    }

    private void PlacePlatform()
    {
        CurrentPlatform.GetComponent<MeshRenderer>().material = _woodOpaque;
        CurrentPlatform.GetComponent<Platform>().enabled = false;
        CurrentPlatform.GetComponent<Collider>().enabled = true;

        if (CurrentPlatform.CompareTag("HalfPlank"))
        {
            LevelManager.HalfPlankCounter--;

            if (LevelManager.HalfPlankCounter <= 0)
            {
                _platformPrefabs.RemoveAt(_currentScrollIndex);
                ModifyIndex();
            }
        }
        else if (CurrentPlatform.CompareTag("CurvedPlank"))
        {
            LevelManager.CurvedPlankCounter--;


            if (LevelManager.CurvedPlankCounter <= 0)
            {
                _platformPrefabs.RemoveAt(_currentScrollIndex);
                ModifyIndex();
            }
        }
        else if (CurrentPlatform.CompareTag("FlatPlank"))
        {
            LevelManager.FlatPlankCounter--;

            if (LevelManager.FlatPlankCounter <= 0)
            {
                _platformPrefabs.RemoveAt(_currentScrollIndex);
                ModifyIndex();
            }
        }
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
                    CurrentPlatform.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
                }
            }
        }

        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 100, Color.blue);
    }

    private void DeleteMode()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * 100, out hit))
        {
            if (hit.collider.GetComponent<Wall>() || hit.collider.GetComponent<Platform>())
            {
                _deletePlatformHelperSphere.transform.position = new Vector3(hit.point.x, hit.point.y, _axisLockValue);
            }
        }
        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 100, Color.red);
    }

    private void ModifyIndex()
    {
        if (_platformPrefabs.Count > 0)
        {
            _currentScrollIndex = 0;
            _platformPrefab = _platformPrefabs[_currentScrollIndex];
        }
        else
        {
            _platformPrefab = null;
            CurrentPlatform = null;
            isDeleteMode = !isDeleteMode;
        }
    }
}