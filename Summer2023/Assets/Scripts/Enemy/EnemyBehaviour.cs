using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int _allCircleCount;
    private int _currCircleCount;
    [SerializeField] private EnemyStatSO _enemyStat;
    [SerializeField] private GameObject _player;
    private GameObject _interactingLine;
    [SerializeField] private PointerOver _ptr;
    private EnemyAI _enemyAI;
    private EnemyShooter _enemyShooter;
    private PointBehaviour[] _circleGameObjects;
    [Header("Point Spawn")]
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private Vector3 _offset;
    public bool IsSpawned = false;
    [SerializeField] private bool _isBigTreant;
    [SerializeField] private GameObject _treantPrefab;
    [Header("Audio")]
    [SerializeField] private GameObject _deathAudioPrefab;
    private bool _gameIsShuttingDown = false;
    void OnApplicationQuit()
    {
        this._gameIsShuttingDown = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint();
        if (_ptr == null) _ptr = GetComponent<PointerOver>();
        _allCircleCount = _enemyStat.NumberOfPoints;
        _currCircleCount = 0;
        _enemyAI = GetComponent<EnemyAI>();
        _enemyShooter = GetComponent<EnemyShooter>();
        _circleGameObjects = GetComponentsInChildren<PointBehaviour>();
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) >= _enemyStat.DespawnDistance) {
            ReturnEnemy();
        }
        // bool temp = _enemyAI.AllowMove;
        if (_interactingLine != null) { _enemyAI.AllowMove = false; } 
        else if (!_enemyShooter.IsCharging) { _enemyAI.AllowMove = true; }
        CheckCircle();
        if (!_enemyShooter.IsCharging) CheckDie();
        if (Input.GetKeyDown(KeyCode.P)) Destroy(gameObject);
    }
    public void UpdateCircle() {
        _currCircleCount = Mathf.Clamp(_currCircleCount + 1, 0, _allCircleCount);
    }

    public void ResetCircle() {
        _currCircleCount = 0;
    }
    private void CheckDie() {
        if (_currCircleCount == _allCircleCount) {
            _interactingLine.GetComponentInParent<Line>().KillLine();
            if (_ptr.enabled) {
                foreach (PointerOver obj in _ptr.Objects) {
                    if (obj.EnemyData == _ptr.EnemyData) { Destroy(obj.gameObject); }
                }
                FindObjectOfType<AbilityHolderForChoose>().Deactivate();
            }
            Destroy(gameObject);
            // if (transform.parent.gameObject != null) { Destroy(transform.parent.gameObject); }
        }
    }
    private void OnDestroy() {
        if (!_gameIsShuttingDown) Instantiate(_deathAudioPrefab, transform.position, Quaternion.identity);
        if (!IsSpawned) {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            es?.OnEnemyKilled();
        }
        if (_isBigTreant) {
            int count = Random.Range(2, 4);
            for (int i = 0; i < count; i++) {
                if (!_gameIsShuttingDown) {
                    GameObject treant = Instantiate(_treantPrefab, transform.position, Quaternion.identity);
                    treant.GetComponent<EnemyBehaviour>().IsSpawned = true;
                }
            }
        }
        ExperienceManager.Instance?.AddExperience(_enemyStat.Exp);
    }
    private void ReturnEnemy() {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = _player.transform.position + es.RelativeSpawnPoints[Random.Range(0, es.RelativeSpawnPoints.Count)].position;
    }
    public void SetInteractingLine(GameObject line) {
        if (_interactingLine == null) {
            _interactingLine = line;
            _interactingLine.GetComponentInParent<Line>().EnemiesInteracting.Add(this.gameObject);
        }
    }

    private void CheckCircle() {
        if (_interactingLine == null) {
            foreach (PointBehaviour obj in _circleGameObjects) {
                obj.TriggerOutline(false);
            }
        }
        else {
            foreach (PointBehaviour obj in _circleGameObjects) {
                obj.TriggerOutline(true);
            }
        }
    }

    private void SpawnPoint() {
        // Debug.Log("A");
        float deg = Random.Range(0, 180);
        int numberOfPoints = _enemyStat.NumberOfPoints;
        float degLeft = 360 / numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++) {
            deg += degLeft;
            float y = Mathf.Sin(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;
            float x = Mathf.Cos(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;
            GameObject point = Instantiate(_pointPrefab, new Vector3(x, y) + transform.position + _offset, Quaternion.identity);
            point.transform.parent = transform;
        }
    }
}
