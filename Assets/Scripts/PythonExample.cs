using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using UnityEngine;

public class PythonExample : MonoBehaviour {

	string pySource = @"
import UnityEngine
from UnityEngine import *
import random
import math

def greet():
	return ""Hello Daniel""

def random_even(start, end):
	return 2 * random.randint(start, end)

# Called every 10th of a second
x, y = 1, 1
counter = 0

def update():
	global x, y, counter

	if counter < y :
		gun_controller.Shoot()
		counter += 1
	elif counter < (y + 10) :
		counter += 1
	else :
		z = x
		x = y
		y = z + x
		counter = 0
";

	ScriptEngine engine;
	dynamic scope;

	public delegate void VoidVoidMethod(float input);
    // Use this for initialization
    void Start()
    {
		engine = Python.CreateEngine();

		//Add the Python standard library
		ICollection<string> searchPaths = engine.GetSearchPaths();
		searchPaths.Add(Application.dataPath + @"\Plugins\Lib\");
		engine.SetSearchPaths(searchPaths);

		engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(GameObject)));

		scope = engine.CreateScope();
		
		engine.Execute(pySource, scope);
		scope.gun_controller = GetComponent<ControllerGun>();
		scope.mov_controller = GetComponent<ControllerMovement>();

        //Debug.Log(scope.greet());
		//Debug.Log(scope.random_even(0,49) + " is a random even");
		
		VoidVoidMethod tp = TestPrint;

		scope.SetVariable("test_print", tp);
    }

    // Update is called once per frame
	float lastTime = 0.0f;
    void Update () {
		if(Time.time - 0.1f > lastTime)
		{
			scope.update();
			lastTime += 0.1f;
		}	
	}

	public void TestPrint(float proportion)
	{
		float temp = proportion * 20;
		Debug.Log(proportion + " of 20 is " + temp);
	}

/*	


Debug.Log(""Hello World from Python!"")

	dynamic py = engine.ExecuteFile(Application.dataPath + @"\greeter.py");
*/

}
