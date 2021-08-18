using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public LayerMask collisionMask;
	float speed = 10.0f;
	float damage = 1.0f;

	float startTime;

	void Start()
	{
		startTime = Time.time;
	}

    void Update()
    {
		if(Time.time - startTime > 5.0f)
		{
			GameObject.Destroy(gameObject);
		}

		float moveStep = speed * Time.deltaTime;
		CheckCollisions(moveStep);
		transform.Translate(Vector3.forward * moveStep);
    }

	void CheckCollisions(float moveStep)
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveStep, collisionMask, QueryTriggerInteraction.Collide))
		{
			IDamageable hitDamageable = hit.collider.GetComponent<IDamageable>();
			if(hitDamageable != null)
			{
				hitDamageable.TakeHit(damage, hit);
			}
			GameObject.Destroy (gameObject);	
		}
	}
}
