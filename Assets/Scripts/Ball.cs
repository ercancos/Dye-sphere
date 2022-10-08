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
    private Rigidbody _rb;
    private Color _color = new Color(1f, 0.3150943f, 0.3150943f);

    public Rigidbody GetBallRB()
    {
        return _rb;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Constraints();
    }


    private void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            if (collide.gameObject.GetComponent<MeshRenderer>().material.color != _color)
            {
                collide.gameObject.GetComponent<MeshRenderer>().material.color = _color;
            }
        }
    }


    private void Constraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
