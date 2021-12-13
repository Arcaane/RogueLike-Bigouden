using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAINTING,
        COUNTING,
        FINISHED
    }

    [System.Serializable]
    public class Wave // Createur de wave d'ennemis
    {
        public string name;
        public Transform[] enemy;
        public int[] count;
        public float rate;
        public int bonusLife;
        public int bonusDamage;
        public int bonusShield;
    }

    public List<GameObject> EnnemiesSpawned;
    public Wave[] waves;
    public int nextWave;
    public LayerMask isEnemy;

    public Transform[] spawnPoints;

    public float timeBetweenWave = 5f;
    public float waveCountDown;
    private float searchCountDown = 1f;
    public SpawnState State = SpawnState.COUNTING;
    public List<Animator> doorAnimator;

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
        if (State == SpawnState.FINISHED) // Plus de wave 
        {
            if (!IsEnemyAlive())
            {
                _roomLoader.GetComponent<RoomLoader>().ClearedRoom(); // Fonction qui ouvre les portes de la salle
            }

            return;
        }

        if (State == SpawnState.WAINTING) // Attente avant de spawn la next wave
        {
            foreach (Animator t in doorAnimator)
            {
                t.SetBool("Spawn", false);
            }

            if (!IsEnemyAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown < 0)
        {
            if (State != SpawnState.SPAWNING) // Spawn la next wave
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                foreach (Animator t in doorAnimator)
                {
                    
                    t.SetBool("Spawn", true);
                }
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
            return;
        }
        else
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

        _esp.gameObject.GetComponent<EnnemyStatsManager>().lifePoint += _wave.bonusLife;
        _esp.gameObject.GetComponent<EnnemyStatsManager>().damageDealt += _wave.bonusDamage;
        _esp.gameObject.GetComponent<EnnemyStatsManager>().shieldPoint += _wave.bonusShield;
    }

    bool IsEnemyAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            Debug.Log("Check");
            searchCountDown = 1;
            EnnemiesSpawned.Clear();

            Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 10f, isEnemy);
            foreach (var ctx in ennemyInRoom)
            {
                EnnemiesSpawned.Add(ctx.gameObject);
            }

            foreach (var _e in EnnemiesSpawned)
            {
                if (_e.GetComponent<EnnemyStatsManager>().lifePoint <= 0)
                {
                    EnnemiesSpawned.Remove(_e);
                }
            }

            if (EnnemiesSpawned.Count == 0)
            {
                return false;
            }
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}