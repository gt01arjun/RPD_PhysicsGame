using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _ingameControls;
    [SerializeField]
    private GameObject _simulationControls;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsPrepMode)
        {
            _ingameControls.SetActive(true);
        }
        else
        {
            _ingameControls.SetActive(false);
        }
    }
}
