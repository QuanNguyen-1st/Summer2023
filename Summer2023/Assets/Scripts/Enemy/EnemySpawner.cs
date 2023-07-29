using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup {
        public string EnemyName;
        public int EnemyCount;          //Total number of a type of enemy
        [HideInInspector] public int SpawnCount;          //Number of a type of enemy currently spawn this wave
        public GameObject EnemyPrefab;
    }
    [System.Serializable]
    public class Wave{
        public string WaveName;
        public List<EnemyGroup> EnemyGroups;  
        [HideInInspector] public int WaveQuota;            //Total number of enemies spawn this wave
        public float SpawnInterval;      //Interval at which to spawn enemies
        [HideInInspector] public int SpawnCount;           //Number of enemies currently spawn this wave
    }
    
    [SerializeField] private List<Wave> _waves;
    private int _currentWaveCount;
    [SerializeField] private GameObject _player;
    
    [Header("Spawner Attributes")]
    // [SerializeField] private float _waveInterval;
    private float _spawnTimer;
    [SerializeField] private int _maxEnemiesAllowed;
    private int _enemiesAlive;
    private bool _maxEnemeisReached = false;

    [Header("Spawn Position")]
    public List<Transform> RelativeSpawnPoints;
    [Header("UI")]
    [SerializeField] private ResultMenu _resultMenu;
    
    

    // Start is called before the first frame update
    void Start()
    {
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWaveCount < _waves.Count && _waves[_currentWaveCount].SpawnCount == _waves[_currentWaveCount].WaveQuota) {
            BeginNextWave();
        }
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _waves[_currentWaveCount].SpawnInterval) {
            _spawnTimer = 0f;
            SpawnEnemies();
        }
        CheckWin();
    }

    private void BeginNextWave() {
        // yield return new WaitForSeconds(_waveInterval);

        if (_currentWaveCount < _waves.Count - 1) {
            _currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    private void CalculateWaveQuota() {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in _waves[_currentWaveCount].EnemyGroups) {
            currentWaveQuota += enemyGroup.EnemyCount;
        }
        _waves[_currentWaveCount].WaveQuota = currentWaveQuota;
        Debug.Log(currentWaveQuota);
    }

    private void SpawnEnemies() {
        if (_waves[_currentWaveCount].SpawnCount < _waves[_currentWaveCount].WaveQuota && !_maxEnemeisReached) {
            foreach (var enemyGroup in _waves[_currentWaveCount].EnemyGroups) {
                if (enemyGroup.SpawnCount < enemyGroup.EnemyCount) {
                    if (_enemiesAlive >= _maxEnemiesAllowed) { 
                        _maxEnemeisReached = true;
                        return;
                    }
                    Instantiate(enemyGroup.EnemyPrefab, _player.transform.position + RelativeSpawnPoints[Random.Range(0, RelativeSpawnPoints.Count)].position, Quaternion.identity);

                    // Vector2 spawnPosition = new Vector2(_player.transform.position.x + Random.Range(-10f, 10f), _player.transform.position.y + Random.Range(-10f, 10f));
                    // Instantiate(enemyGroup.EnemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.SpawnCount++;
                    _waves[_currentWaveCount].SpawnCount++;
                    _enemiesAlive++;
                }
            }
        }
        if (_enemiesAlive < _maxEnemiesAllowed) {
            _maxEnemeisReached = false;
        }
    }
    public void OnEnemyKilled() {
        _enemiesAlive--;
    }

    private void CheckWin() {
        if ((_currentWaveCount == _waves.Count - 1) && _waves[_currentWaveCount].SpawnCount == _waves[_currentWaveCount].WaveQuota && _enemiesAlive == 0) {
            _resultMenu.WinGame(_player.GetComponent<PlayerExperience>().CurrentExperience);
        }
    }
}
