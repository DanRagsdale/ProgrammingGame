using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython.Runtime;

[RequireComponent (typeof (ControllerMovement))]
[RequireComponent (typeof (ControllerGun))]
public class PyControllerTurret : PyController
{
	ControllerGun controllerGun;
	ControllerMovement controllerMovement;

	string PySource = @"
import UnityEngine
from UnityEngine import *
import random

def greet():
	return ""Hello Daniel""

def random_even(start, end):
	return 2 * random.randint(start, end)

# Called every 10th of a second
def update():
	enemies = world_object.get_enemies()	
	if len(enemies) > 0 :
		world_object.look_at(enemies[0])
		world_object.shoot()
";

    protected override void Start()
    {
		base.Start();

		controllerGun = GetComponent<ControllerGun>(); 
		controllerMovement = GetComponent<ControllerMovement>();

		engine.Execute(PySource, scope);
		scope.world_object = new PyObjectTurret(controllerMovement, controllerGun);
    }
	
	public class PyObjectTurret
	{
		ControllerMovement cm;	
		ControllerGun cg;

		public PyObjectTurret(ControllerMovement _cm, ControllerGun _cg)
		{
			cm = _cm;
			cg = _cg;
		}

		public void shoot()
		{
			cg.Shoot();
		}

		public void look_at(float x, float y, float z)
		{
			cm.LookAt(new Vector3(x, y, z));
		}

		public void look_at(Vector3 vec)
		{
			cm.LookAt(vec);
		}
	
		public Vector3[] get_enemies()
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			Vector3[] positions = new Vector3[enemies.Length];

			for(int i = 0; i < enemies.Length; i++)
			{
				positions[i] = enemies[i].transform.position;
			}

			return positions;
		}
	}
}
