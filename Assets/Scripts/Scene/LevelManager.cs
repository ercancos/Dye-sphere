//Libraries..
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * This script is responsible of scene level management.
 * 
 */


public class LevelManager : MonoBehaviour
{
    #region Variables

    private int _totalNumberOfLevels = 6;
    private int _totalNumberOfIgnoredLevels = 2;
    private int _currentLevel;

    #endregion

    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("currentLevel");

        if (_currentLevel <= 0)
            _currentLevel = 2; //It starts level-1 of regular game mode.
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        PlayerPrefs.SetInt("currentLevel", _currentLevel);

        int currentLevelMod;

        if (_currentLevel < _totalNumberOfLevels)     // Level 0 and Level 1 are ignored.
        {
            currentLevelMod = _currentLevel;
        }
        else
        {
            currentLevelMod = ((_currentLevel - _totalNumberOfIgnoredLevels) % (_totalNumberOfLevels - _totalNumberOfIgnoredLevels)) + _totalNumberOfIgnoredLevels;
        }


        SceneManager.LoadScene(currentLevelMod);
    }

    public void LoadExperimentalLevel()
    {
        SceneManager.LoadScene("Experimental_Level");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Entrance");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
