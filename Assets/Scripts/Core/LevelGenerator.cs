//Libraries..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script is responsible for generating game levels by using static parameters.
 * 
 */


public class LevelGenerator : MonoBehaviour
{
    #region Variables
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject floorPiecePrefab;
    [SerializeField] private GameObject wallPrefab;

    [Space]
    [Header("Static Parameters")]
    [Tooltip("Level size(as a square) should be greater than inner-wall count.")]
    [SerializeField] private int levelSize = 8;
    [SerializeField] private int wallNumber = 6;

    private GameObject _groundParent;
    private GameObject _wallsParent;

    private int[,,] objectPositions;

    #endregion


    private void Awake()
    {
        if (CheckStaticParameters())
            return;

        InitializeParents();
        InitializeLevel();
    }


    private bool CheckStaticParameters()
    {
        if (levelSize * levelSize < wallNumber)
        {
            Debug.LogWarning("Level size should be greater than inner-wall count.");
            return true;
        }
        return false;
    }


    private void InitializePlayer()
    {
        bool isSuccess = false;
        int i, j;

        while (!(isSuccess))
        {
            i = UnityEngine.Random.Range(0, levelSize);
            j = UnityEngine.Random.Range(0, levelSize);
            if (objectPositions[i, j, 0] == 0)
            {
                Instantiate(playerPrefab, new Vector3(i, 0.5f, j), Quaternion.identity);
                isSuccess = true;
            }
        }

    }


    private void InitializeParents()
    {
        _groundParent = new GameObject
        {
            name = "Ground"
        };

        _wallsParent = new GameObject
        {
            name = "WallsParent"
        };
    }


    private void InitializeLevel()
    {
        objectPositions = new int[levelSize, levelSize, 1];

        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                objectPositions[i, j, 0] = 0;
            }
        }

        GenerateInnerWalls();
        FillSpacesWithFloorPieces();
        SurroundWithWalls();
        InitializePlayer();
    }


    private void GenerateInnerWalls()
    {
        int tempWallNumber = wallNumber;
        while (tempWallNumber > 0)
        {
            int randomizedPosition_X = UnityEngine.Random.Range(0, levelSize);
            int randomizedPosition_Z = UnityEngine.Random.Range(0, levelSize);

            if (objectPositions[randomizedPosition_X, randomizedPosition_Z, 0] == 0)
            {
                GameObject tempObj = Instantiate(wallPrefab, new Vector3(randomizedPosition_X, 0.5f, randomizedPosition_Z), Quaternion.identity);
                tempObj.name = "innerWall";
                tempObj.transform.SetParent(_wallsParent.transform);
                objectPositions[randomizedPosition_X, randomizedPosition_Z, 0] = 1;
                tempWallNumber--;
            }
        }
    }


    private void FillSpacesWithFloorPieces()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                if (objectPositions[i, j, 0] == 0)
                {
                    GameObject tempObj = Instantiate(floorPiecePrefab, new Vector3(i, 0f, j), new Quaternion(0.707106829f, 0f, 0f, 0.707106829f));//for x=90 degree.
                    tempObj.transform.SetParent(_groundParent.transform);
                }
            }
        }
    }


    private void SurroundWithWalls()
    {
        List<GameObject> wallsList = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            wallsList.Add(Instantiate(wallPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity, _wallsParent.transform));
        }

        wallsList[0].transform.localScale = new Vector3(1f, 1f, levelSize);
        wallsList[0].transform.position = new Vector3(-1f, 0.5f, (levelSize - 1) / 2f);
        wallsList[1].transform.localScale = new Vector3(1f, 1f, levelSize);
        wallsList[1].transform.position = new Vector3(levelSize, 0.5f, (levelSize - 1) / 2f);

        wallsList[2].transform.localScale = new Vector3(1f, 1f, levelSize + 2);
        wallsList[2].transform.rotation = new Quaternion(0f, 0.707106829f, 0f, 0.707106829f);//for y=90 degree.
        wallsList[2].transform.position = new Vector3((levelSize - 1) / 2f, 0.5f, -1f);
        wallsList[3].transform.localScale = new Vector3(1f, 1f, levelSize + 2);
        wallsList[3].transform.rotation = new Quaternion(0f, 0.707106829f, 0f, 0.707106829f);//for y=90 degree.
        wallsList[3].transform.position = new Vector3((levelSize - 1) / 2f, 0.5f, levelSize);
    }



    private IEnumerator AddWalls()
    {
        yield return null;
    }
}
