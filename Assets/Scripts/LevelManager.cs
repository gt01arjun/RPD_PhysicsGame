using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static UnityEvent GameLose = new UnityEvent();
    public static UnityEvent GameWin = new UnityEvent();

    public static bool IsGameOver;

    [SerializeField]
    private GameObject _winScreen;

    [SerializeField]
    private GameObject _loseScreen;

    [SerializeField]
    private GameObject[] _levels;

    private int _currentLevel;
    private int _maxLevel;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        GameLose.AddListener(Lose);
        GameWin.AddListener(Win);
        IsGameOver = false;
        _currentLevel = PlayerPrefs.GetInt("CURRENTLEVEL");
        _maxLevel = PlayerPrefs.GetInt("MAXLEVEL");

        _levels[_currentLevel - 1].SetActive(true);
    }

    private void Win()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        _winScreen.SetActive(true);
        if (_currentLevel <= 3)
        {
            if (_currentLevel < _maxLevel)
            {

            }
            else
            {
                _currentLevel++;
                _maxLevel = _currentLevel;
                PlayerPrefs.SetInt("MAXLEVEL", _maxLevel);
                PlayerPrefs.SetInt("CURRENTLEVEL", _currentLevel);
            }
        }
        Time.timeScale = 0f;
    }

    private void Lose()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        _loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}