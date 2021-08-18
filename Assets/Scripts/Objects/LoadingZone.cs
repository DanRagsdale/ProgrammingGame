using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
	public float loadAmount = 9.0f;
	public float loadPeriod = 1.0f;

	public bool isLoadZone = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	float lastLoadTime;
	void OnTriggerStay(Collider col)
	{
		ILoadable loadable = col.GetComponent<ILoadable>();
		if(loadable != null && Time.time - loadPeriod > lastLoadTime)
		{
			if(isLoadZone)
			{
				loadable.LoadCargo(loadAmount);
				lastLoadTime = Time.time;
			} else {
				loadable.UnloadCargo(loadAmount);
				lastLoadTime = Time.time;
			}
		}
	}
}
