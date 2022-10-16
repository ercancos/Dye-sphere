//Libraries..
using UnityEngine;

/*
 * 
 * This script is responsible for movement of ball.
 * 
 */


[RequireComponent(typeof(Ball))]
public class Movement : MonoBehaviour
{
    #region Variables
    public delegate void MovementAction();
    public static event MovementAction MovementStarted;

    [SerializeField] private float moveSpeed;

    private bool isMovementStart = false;
    private bool isLevelEnd = false;

    private Vector2 _firstPos;
    private Vector2 _secondPos;
    private Vector2 _currentPos;

    private Ball _scriptInstance;
    private Rigidbody _rb;

    #endregion

    private void OnEnable()
    {
        GameManager.LevelCompleted += StopMovement;
    }

    private void Start()
    {
        _scriptInstance = GetComponent<Ball>();
        _rb = _scriptInstance.GetBallRB();
    }

    private void Update()
    {
        if (!(isLevelEnd))
        {
            DetectSwipe();
        }
    }

    private void FixedUpdate()
    {
        if (!(isLevelEnd))
        {
            MoveBall();
        }
    }

    private void DetectSwipe()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (!(isMovementStart))
            {
                isMovementStart = true;
                if (MovementStarted != null)
                {
                    MovementStarted();
                }
            }

            _firstPos = new Vector2(mousePosition.x, mousePosition.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _secondPos = new Vector2(mousePosition.x, mousePosition.y);

            _currentPos = new Vector2(
                (_secondPos.x - _firstPos.x),
                (_secondPos.y - _firstPos.y)
            );

            _currentPos.Normalize();
        }
    }

    private void MoveBall()
    {
        if (_currentPos.y > 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            //Forward Movement
            _rb.velocity = Vector3.forward * moveSpeed;
        }
        else if (_currentPos.y < 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            //Backward Movement
            _rb.velocity = Vector3.back * moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            //Right Movement
            _rb.velocity = Vector3.right * moveSpeed;
        }
        else if (_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            //Left Movement
            _rb.velocity = Vector3.left * moveSpeed;
        }
    }

    private void StopMovement()
    {
        isLevelEnd = true;
    }

    private void OnDestroy()
    {
        GameManager.LevelCompleted -= StopMovement;
    }
}
