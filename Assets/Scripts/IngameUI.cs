using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _ingameControls;
    [SerializeField]
    private GameObject _simulationControls;

    [SerializeField]
    private GameManager _manager;

    // Update is called once per frame
    void Update()
    {
        if (_manager._isPrepMode)
        {
            _ingameControls.SetActive(true);
        }
        else
        {
            _ingameControls.SetActive(false);
        }
    }
}
