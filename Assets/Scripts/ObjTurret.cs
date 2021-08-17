using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ControllerMovement))]
[RequireComponent (typeof (ControllerGun))]
public class ObjTurret : ObjSelectable
{
	Camera viewCamera;

	ControllerMovement controller;
	ControllerGun controllerGun;

    protected override void Start()
    {
		base.Start();

		viewCamera = Camera.main;
		controller = GetComponent<ControllerMovement>();
		controllerGun = GetComponent<ControllerGun> ();
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
				//Debug.DrawLine(transform.position, point, Color.red);
				controller.LookAt(point);
			}

			if(Input.GetMouseButton(0))
			{					
				controllerGun.Shoot();
			}
		}
    }
}
