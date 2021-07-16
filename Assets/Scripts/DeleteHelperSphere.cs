using UnityEngine;

public class DeleteHelperSphere : MonoBehaviour
{
    private GameObject _deleteThisObject;
    private bool _canDelete;

    [SerializeField]
    private AudioSource _plankRemovedAudioSource;

    private void Start()
    {
        _canDelete = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Platform>())
        {
            _canDelete = true;
            _deleteThisObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Platform>())
        {
            _canDelete = false;
            _deleteThisObject = null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canDelete && _deleteThisObject != null)
        {
            if (_deleteThisObject.CompareTag("HalfPlank"))
            {
                LevelManager.HalfPlankCounter++;
                if (LevelManager.HalfPlankCounter == 1)
                {
                    PlatformSpawner.AddToPrefabList.Invoke("HalfPlank");
                }
            }
            else if (_deleteThisObject.CompareTag("CurvedPlank"))
            {
                LevelManager.CurvedPlankCounter++;
                if (LevelManager.CurvedPlankCounter == 1)
                {
                    PlatformSpawner.AddToPrefabList.Invoke("CurvedPlank");
                }
            }
            else if (_deleteThisObject.CompareTag("FlatPlank"))
            {
                LevelManager.FlatPlankCounter++;
                if (LevelManager.FlatPlankCounter == 1)
                {
                    PlatformSpawner.AddToPrefabList.Invoke("FlatPlank");
                }
            }

            _plankRemovedAudioSource.Stop();
            _plankRemovedAudioSource.Play();

            Destroy(_deleteThisObject);
        }
    }
}