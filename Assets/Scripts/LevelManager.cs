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

    [SerializeField]
    private LevelData[] _levelData;

    [SerializeField]
    private TMP_Text _curvedPlankText;

    [SerializeField]
    private TMP_Text _halfPlankText;

    [SerializeField]
    private TMP_Text _flatPlankText;

    [SerializeField]
    private TMP_Text _retriesLeftText;

    private int _currentLevel;
    private int _maxLevel;

    public static int CurvedPlankCounter;
    public static int HalfPlankCounter;
    public static int FlatPlankCounter;

    public static int RetriesLeftCounter;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        GameLose.AddListener(Lose);
        GameWin.AddListener(Win);
        IsGameOver = false;
        //_currentLevel = PlayerPrefs.GetInt("CURRENTLEVEL");
        //_maxLevel = PlayerPrefs.GetInt("MAXLEVEL");

        //_levels[_currentLevel - 1].SetActive(true);

        //CurvedPlankCounter = _levelData[_currentLevel - 1].CurvedPlankCounter;
        //HalfPlankCounter = _levelData[_currentLevel - 1].HalfPlankCounter;
        //FlatPlankCounter = _levelData[_currentLevel - 1].FlatPlankCounter;

        CurvedPlankCounter = 3;
        HalfPlankCounter = 3;
        FlatPlankCounter = 3;
        RetriesLeftCounter = 3;
    }

    private void Update()
    {
        _curvedPlankText.text = $"CURVED PLANKS     {CurvedPlankCounter}";
        _halfPlankText.text = $"HALF PLANKS         {HalfPlankCounter}";
        _flatPlankText.text = $"FLAT PLANKS          {FlatPlankCounter}";
        _retriesLeftText.text = $"RETRIES LEFT          {RetriesLeftCounter}";
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
                ////PlayerPrefs.SetInt("MAXLEVEL", _maxLevel);
                // PlayerPrefs.SetInt("CURRENTLEVEL", _currentLevel);
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