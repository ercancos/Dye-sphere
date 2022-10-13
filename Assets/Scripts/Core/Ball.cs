//Libraries..
using UnityEngine;

/*
 * 
 * This script is responsible of ball interactions and connections.
 * 
 */


[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    #region Variables

    public delegate void DyeAction();
    public static event DyeAction GroundPainted;

    private Rigidbody _rb;
    private Color _color = new Color(1f, 0.3150943f, 0.3150943f);

    #endregion


    public Rigidbody GetBallRB()
    {
        return _rb;
    }

    private void OnEnable()
    {
        Movement.MovementStarted += Constraints;
    }


    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            if (collide.gameObject.GetComponent<MeshRenderer>().material.color != _color)
            {
                collide.gameObject.GetComponent<MeshRenderer>().material.color = _color;
                if (GroundPainted != null)
                {
                    GroundPainted();
                }
            }
        }
    }


    private void Constraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }


    private void OnDestroy()
    {
        Movement.MovementStarted -= Constraints;
    }
}
