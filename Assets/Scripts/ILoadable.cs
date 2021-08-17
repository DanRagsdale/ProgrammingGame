using UnityEngine;

public interface ILoadable
{
	float LoadCargo(float maxAmount);

	float UnloadCargo(float maxAmount); 
}
