using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Wave[] waves;
	public Enemy enemy;

	[System.Serializable]
	public class Wave
	{
		public int enemyCount;
		public float spawnPeriod;
	}	

	Wave currentWave;
	int waveNum;

	int enemiesRemainingToSpawn;
	int enemiesAlive;
	float nextSpawnTime;

	void Start()
	{
		NextWave();
	}

	void Update()
	{
		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
		{
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.spawnPeriod;

			Enemy spawnedEnemy = Instantiate (enemy, Vector3.zero, Quaternion.identity) as Enemy;
			spawnedEnemy.OnDeath += OnEnemyDeath;
		}
	}

	void OnEnemyDeath()
	{
		enemiesAlive--;
		if(enemiesAlive == 0)
		{
			NextWave();
		}
	}

	void NextWave()
	{
		if(waveNum < waves.Length)
		{
			waveNum++;
			currentWave = waves[waveNum - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesAlive = currentWave.enemyCount;
		}
	}
}
