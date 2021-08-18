using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ControllerMovement))]
public class ObjTruck : ObjSelectable
{
	public float moveSpeed = 3.0f;

	Camera viewCamera;
	ControllerMovement controller;

    protected override void Start()
    {
		base.Start();

		viewCamera = Camera.main;
		controller = GetComponent<ControllerMovement>();
    }

    protected override void Update()
    {
		base.Update();

		if(selected)
		{
			Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			float rayDistance;

			if (groundPlane.Raycast(ray, out rayDistance))
			{
				Vector3 point = ray.GetPoint(rayDistance);
				controller.LookAt(point);
			}

			Vector3 moveInput = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
			Vector3 moveVelocity = moveInput.normalized * moveSpeed;
			controller.MoveRelative (moveVelocity);
		} else {
			controller.StopMovement();
		}
    }
}
