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
            if (collide.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                collide.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }


    private void Constraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
