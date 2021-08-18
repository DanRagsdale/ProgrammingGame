using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using IronPython.Runtime;
using UnityEngine;

public abstract class PyController : MonoBehaviour {

	protected ScriptEngine engine;
	protected dynamic scope;

	//public delegate void VoidPyTupMethod(PythonTuple input);
	
	protected virtual void Start()
    {
		engine = Python.CreateEngine();

		//Add the Python standard library
		ICollection<string> searchPaths = engine.GetSearchPaths();
		searchPaths.Add(Application.dataPath + @"\Plugins\Lib\");
		engine.SetSearchPaths(searchPaths);

		engine.Runtime.LoadAssembly(Assembly.GetAssembly(typeof(GameObject)));

		scope = engine.CreateScope();
    }

	float lastTime = 0.0f;
	bool badScript = false;
    protected virtual async Task Update () {
		if(Time.time - 0.1f > lastTime && !badScript)
		{
			CancellationTokenSource s_cts = new CancellationTokenSource();
			try
			{
				s_cts.CancelAfter(100);
				await UpdatePyAsync();
			}
			catch (System.Exception)
			{
				badScript = true;
				Debug.Log("Python Update aborted!!!!");
			}
			finally
			{
				s_cts.Dispose();
			}
		}	
	}

	async Task UpdatePyAsync()
	{
		scope.update();
	}
}
