using UnityEngine;

public class DeleteHelperSphere : MonoBehaviour
{
    private GameObject _deleteThisObject;
    private bool _canDelete;

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
        if (Input.GetMouseButtonDown(0) && _canDelete)
        {
            Destroy(_deleteThisObject);

            if (_deleteThisObject.CompareTag("HalfPlank"))
            {
                LevelManager.HalfPlankCounter++;
            }
            else if (_deleteThisObject.CompareTag("CurvedPlank"))
            {
                LevelManager.CurvedPlankCounter++;
            }
            else if (_deleteThisObject.CompareTag("FlatPlank"))
            {
                LevelManager.FlatPlankCounter++;
            }
        }
    }
}