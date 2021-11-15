using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
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
        public Transform enemy;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    public int nextWave;

    public Transform[] spawnPoints;
    
    public float timeBetweenWave = 5f;
    public float waveCountDown;
    private float searchCountDown = 1f;
    public SpawnState State = SpawnState.COUNTING;
    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points assigned!");
        }
        waveCountDown = timeBetweenWave;
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

    void WaveCompleted()
    {
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
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        State = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
           SpawnEnemy(_wave.enemy);
           yield return new WaitForSeconds(1f / _wave.rate);
        }
        State = SpawnState.WAINTING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Ennemy: " + _enemy.name);

        //Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, transform.position, transform.rotation);
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

[CustomEditor(typeof(DecalMeshHelperEditor))]
class DecalMeshHelperEditor : Editor {
    public override void OnInspectorGUI() {
        if(GUILayout.Button("Wave Cleaner"))
            Debug.Log("It's alive: " + target.name);
    }
}
