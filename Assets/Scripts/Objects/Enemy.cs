using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity 
{
	public enum State
	{
		Idle, Chasing, Attacking
	};

	public float attackDistanceThreshold = 0.5f;
	public float attackPeriod = 1.0f;
	public float attackDamage = 1.0f;

	State currentState;

	UnityEngine.AI.NavMeshAgent pathfinder;
	Transform target;
	LivingEntity targetEntity;
	Material skinMaterial;

	float nextAttackTime;
	float myCollisionRadius;
	float targetCollisionRadius;
	bool hasTarget;

	Color originalColor;

    protected override void Start()
    {
		base.Start();
		pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
		skinMaterial = GetComponent<Renderer> ().material;

		originalColor = skinMaterial.color;

		if(GameObject.FindGameObjectWithTag("Player") != null) {
			hasTarget = true;
			currentState = State.Chasing;
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntity = target.GetComponent<LivingEntity>();
			targetEntity.OnDeath += OnTargetDeath;
		}

		myCollisionRadius = GetComponent<CapsuleCollider>().radius;
		targetCollisionRadius = 0.8f;

		StartCoroutine (UpdatePath ());
    }

	void Update ()
	{
		if(hasTarget)
		{
			if(Time.time > nextAttackTime)
			{				
				float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
				if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
				{					
					nextAttackTime = Time.time + attackPeriod;
					StartCoroutine(Attack());
				}
			}
		} 
	}

	void OnTargetDeath()
	{
			hasTarget = false;
			currentState = State.Idle;
	}

	float attackSpeed = 3.0f;

	IEnumerator Attack()
	{									
		currentState = State.Attacking;
		pathfinder.enabled = false;

		Vector3 originalPosition = transform.position;
		Vector3 attackOffset = (target.position - transform.position).normalized * (targetCollisionRadius * 0.2f + myCollisionRadius);
		Vector3 attackPosition = target.position - attackOffset;

		float percent = 0;

		skinMaterial.color = Color.green;
		bool hasAppliedDamage = false;

		while (percent <= 1)
		{				
			if(percent >= 0.5f && !hasAppliedDamage)
			{
				hasAppliedDamage = true;
				targetEntity.TakeDamage(attackDamage);
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-percent * percent + percent) * 4;

			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

			yield return null;
		}

		skinMaterial.color = originalColor;

		pathfinder.enabled = true;
		currentState = State.Chasing;
	}

	IEnumerator UpdatePath()
	{
		float refreshRate = 0.2f;

		while (hasTarget) 
		{
			if(currentState == State.Chasing)
			{
				Vector3 positionOffset = (target.position - transform.position).normalized * (targetCollisionRadius + myCollisionRadius + attackDistanceThreshold / 2);
				Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z) - positionOffset;
				if(!dead)
				{
					pathfinder.SetDestination (targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
