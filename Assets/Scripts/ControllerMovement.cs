using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class ControllerMovement : MonoBehaviour
{
	Rigidbody rb;

	Vector3 velocity;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
		velocity = _velocity;
    }

    public void MoveRelative(Vector3 relativeVelocity)
    {
		velocity = rb.rotation * relativeVelocity;
    }

	public void StopMovement()
	{
		velocity = Vector3.zero;
	}

    void FixedUpdate()
    {
   		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
   	}

	public void LookAt(float x, float y, float z)
	{
		Debug.Log("Looking at: (" + x + ", " + y + ", " + z + ")");

		Vector3 levelPoint = new Vector3(x, transform.position.y, z);
		transform.LookAt(levelPoint);
	}

	public void LookAt(Vector3 lookPoint)
	{
		Vector3 levelPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt(levelPoint);
	}
}
