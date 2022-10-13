//Libraries..
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * This script is responsible for maintaining and managing game logic.
 * 
 */


public class GameManager : MonoBehaviour
{
    #region Variables
    public delegate void LevelAction();
    public static event LevelAction LevelCompleted;

    private GameObject[] _grounds;
    private float _groundNumbers;
    private float _paintedGroundNumbers;

    #endregion

    private void OnEnable()
    {
        Ball.GroundPainted += IncreasePaintedGroundNum;
    }

    private void Start()
    {
        _grounds = GameObject.FindGameObjectsWithTag("Ground");
        _paintedGroundNumbers = 0;
        _groundNumbers = _grounds.Length;
    }


    private void IncreasePaintedGroundNum()
    {
        _paintedGroundNumbers++;
        if (_paintedGroundNumbers >= _groundNumbers)
        {
            if (LevelCompleted != null)
            {
                LevelCompleted();
            }
        }
    }

    private void OnDestroy()
    {
        Ball.GroundPainted -= IncreasePaintedGroundNum;
    }

}
