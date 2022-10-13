//Libraries..
using UnityEngine;


/*
 * 
 * This script is responsible of UI operations of the entrance scene.
 * 
 */


public class Entrance_UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject regularModeButton;
    [SerializeField] private GameObject experimentalModeButton;
    [SerializeField] private GameObject turnBackButton;
    #endregion


    public void SetStateGameModeFrame(bool isActive)
    {
        SetStateMenuButtons(!(isActive));
        SetStateGameModebuttons(isActive);
    }


    private void Start()
    {
        SetStateMenuButtons(true);
        SetStateGameModebuttons(false);
    }

    private void SetStateMenuButtons(bool isActive)
    {
        playButton.SetActive(isActive);
        optionsButton.SetActive(isActive);
        exitButton.SetActive(isActive);
    }

    private void SetStateGameModebuttons(bool isActive)
    {
        regularModeButton.SetActive(isActive);
        experimentalModeButton.SetActive(isActive);
        turnBackButton.SetActive(isActive);
    }
}
