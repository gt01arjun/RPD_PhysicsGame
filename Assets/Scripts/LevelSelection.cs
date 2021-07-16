using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _levels;

    private int _currentLevel;
    private int _maxLevel;

    private void OnEnable()
    {
        Time.timeScale = 1f;

        _maxLevel = 1;

        if (PlayerPrefs.HasKey("MAXLEVEL"))
        {
            _maxLevel = PlayerPrefs.GetInt("MAXLEVEL");
        }
        else
        {
            PlayerPrefs.SetInt("MAXLEVEL", _maxLevel);
        }

        for (int i = 0; i < _maxLevel; ++i)
        {
            //Unlocked Levels
            _levels[i].GetComponent<Image>().color = Color.white;
            _levels[i].GetComponent<Button>().enabled = true;
        }

        for (int i = _maxLevel; i < _levels.Length; ++i)
        {
            //Locked Levels
            _levels[i].GetComponent<Image>().color = Color.red;
            _levels[i].GetComponent<Button>().enabled = false;
        }
    }

    public void OnLevelSelect(TMP_Text text)
    {
        _currentLevel = Convert.ToInt32(text.text);
        PlayerPrefs.SetInt("CURRENTLEVEL", _currentLevel);
        SceneManager.LoadScene("Game");
    }
}