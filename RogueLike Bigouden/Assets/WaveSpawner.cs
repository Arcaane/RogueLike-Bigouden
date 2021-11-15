using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAINTING, COUNTING, FINISHED }
    
    [System.Serializable]
    public class Wave 
    {
        public string name;
        public Transform[] enemy;
        public int[] count;
        public float rate;
        public int bonusLife;
        public int bonusDamage;
        public int bonusShield;
        public float bonusMovementSpeed;

    }

    public List<GameObject> EnnemiesSpawned;
    public Wave[] waves;
    public int nextWave;

    public Transform[] spawnPoints;
    
    public float timeBetweenWave = 5f;
    public float waveCountDown;
    private float searchCountDown = 1f;
    public SpawnState State = SpawnState.COUNTING;

    public GameObject _roomLoader;
    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points assigned!");
        }
    }

    void Update()
    {
        if (State == SpawnState.FINISHED)
        {
            return;
        }
        
        if (State == SpawnState.WAINTING)
        {
            if (!IsEnemyAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (!(waveCountDown <= 0)){
            if (State != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]) );
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    public void WaveCompleted()
    {
        if (State == SpawnState.FINISHED)
        {
            _roomLoader.GetComponent<RoomLoader>().ClearedRoom();
            Debug.Log("WTF BRO IT'S OVER");
            return;
        }
        else {
            Debug.Log("Wave Completed!");
            State = SpawnState.COUNTING;
            waveCountDown = timeBetweenWave;

            if (nextWave + 1 > waves.Length - 1)
            {
                nextWave = 0;
                Debug.Log("All waves completed");
                State = SpawnState.FINISHED;
            }
            else
            {
                nextWave++;
            }
        }
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        State = SpawnState.SPAWNING;

        for (int i = _wave.enemy.Length - 1; i >= 0; i--)
        {
            int nbEnemies = _wave.count[i];
            for (int j = nbEnemies - 1; j >= 0; j--)
            {
                SpawnEnemy(_wave.enemy[i], _wave);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
        
        
        State = SpawnState.WAINTING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy, Wave _wave)
    {
        Debug.Log("Spawning Ennemy: " + _enemy.name);

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform _esp = Instantiate(_enemy, sp.transform.position, sp.transform.rotation);
        _esp.gameObject.GetComponent<EnnemyStatsManager>().TakeHeal(_wave.bonusLife);
        _esp.gameObject.GetComponent<EnnemyStatsManager>().damageDealt += _wave.bonusDamage; 
        _esp.gameObject.GetComponent<EnnemyStatsManager>().shieldPoint += _wave.bonusShield; 
        _esp.gameObject.GetComponent<EnnemyStatsManager>().movementSpeed += _wave.bonusMovementSpeed; 
        EnnemiesSpawned.Add(_esp.gameObject);
    }

    bool IsEnemyAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1;
            if (GameObject.FindGameObjectsWithTag("Ennemy") == null)
            {
                return false;
            }
        }
        return true;
    }
}


