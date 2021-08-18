using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTransport : MonoBehaviour, ILoadable
{
	public float capacity = 100.0f;
	public float startingLoad = 0.0f;

	float currentLoad;

	float lastTime = 0.0f;

	void Start()
	{
		currentLoad = startingLoad;
	}

    void Update()
    {
		if(Time.time - 0.3f > lastTime)
		{
			//Debug.Log(currentLoad);
		}
    }

	public float LoadCargo(float maxAmount)
	{
		if(currentLoad + maxAmount < capacity) {
			currentLoad += maxAmount;
			return maxAmount;
		} else {
			return 0.0f;
		}
	}

	public float UnloadCargo(float maxAmount)
	{
		if(currentLoad - maxAmount > 0)
		{
			currentLoad -= maxAmount;
			return maxAmount;
		} else if(currentLoad > 0) {
			float tempLoad = currentLoad;
			currentLoad = 0.0f;
			return tempLoad;
		} else {
			return 0.0f;
		}
	}
}
