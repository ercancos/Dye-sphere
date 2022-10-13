//Libraries..
using UnityEngine;


/*
 * 
 * This script is responsible of UI operations of the level/in-game scenes.
 * 
 */


public class Level_UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenuBG;
    [SerializeField] private GameObject levelSuccessFrame;
    #endregion

    public void SetStatePauseMenuFrame(bool isActive)
    {
        SetStatePausebutton(!(isActive));
        SetStatePauseMenuButtons(isActive);
    }

    private void OnEnable()
    {
        GameManager.LevelCompleted += ActivateLevelSuccessFrame;
    }

    private void Start()
    {
        SetStatePausebutton(true);
        SetStatePauseMenuButtons(false);
        levelSuccessFrame.SetActive(false);
    }

    private void SetStatePauseMenuButtons(bool isActive)
    {
        continueButton.SetActive(isActive);
        optionsButton.SetActive(isActive);
        mainMenuButton.SetActive(isActive);
        pauseMenuBG.SetActive(isActive);
    }

    private void SetStatePausebutton(bool isActive)
    {
        pauseButton.SetActive(isActive);
    }

    private void ActivateLevelSuccessFrame()
    {
        levelSuccessFrame.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.LevelCompleted -= ActivateLevelSuccessFrame;
    }
}
