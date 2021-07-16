using UnityEngine;

public class IngameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _ingameControls;
    [SerializeField]
    private GameObject _simulationControls;

    private void Update()
    {
        if (LevelManager.IsPrepMode)
        {
            _ingameControls.SetActive(true);
        }
        else
        {
            _ingameControls.SetActive(false);
        }
    }
}