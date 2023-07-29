using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrainChunks;
    public GameObject Player;
    [SerializeField] private float _checkerRadius;
    // private Vector3 _noTerrainPosition;
    [SerializeField] private LayerMask _terrainMask;
    private PlayerMovement _pm;
    public GameObject CurrentChunk;
    [Header("Optimization")]
    [SerializeField] private List<GameObject> _spawnedChunks;
    // private GameObject _latestChunk;
    [SerializeField] private float _maxOpDist; // > w and h
    // [SerializeField] private float _opCooldonwDuration;
    // private float _opCooldown;
    // Start is called before the first frame update
    void Start()
    {
        _pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkOptimazation();
        ChunkChecker();
        // Debug.Log(_pm.MoveInput);
    }

    private void ChunkChecker() {
        if (!CurrentChunk) {
            return;
        }
        // if (_pm.MoveInput.x != 0 || _pm.MoveInput.y != 0) {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Up").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Up").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Down").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Down").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Up").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Up").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Down").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Down").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Up").position;
                SpawnChunk(noTerrainPosition);
            }
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down").position, _checkerRadius, _terrainMask)) {
                Vector3 noTerrainPosition = CurrentChunk.transform.Find("Down").position;
                SpawnChunk(noTerrainPosition);
            }

        // }
        // if (_pm.MoveInput.x > 0) {
        //     if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right").position, _checkerRadius, _terrainMask)) {
        //         Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right").position;
        //         SpawnChunk(noTerrainPosition);
        //     }
        //     if (_pm.MoveInput.y > 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        //     else if (_pm.MoveInput.y < 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        // }
        // else if (_pm.MoveInput.x < 0) {
        //     if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left").position, _checkerRadius, _terrainMask)) {
        //         Debug.Log(_pm.MoveInput);
        //         Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left").position;
        //         SpawnChunk(noTerrainPosition);
        //     }
        //     if (_pm.MoveInput.y > 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        //     else if (_pm.MoveInput.y < 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        // }
        // if (_pm.MoveInput.y > 0) {
        //     if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up").position, _checkerRadius, _terrainMask)) {
        //         Vector3 noTerrainPosition = CurrentChunk.transform.Find("Up").position;
        //         SpawnChunk(noTerrainPosition);
        //     }
        //     if (_pm.MoveInput.x > 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        //     else if (_pm.MoveInput.x < 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Up").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Up").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        // }
        // else if (_pm.MoveInput.y < 0) {
        //     if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down").position, _checkerRadius, _terrainMask)) {
        //         Vector3 noTerrainPosition = CurrentChunk.transform.Find("Down").position;
        //         SpawnChunk(noTerrainPosition);
        //     }
        //     if (_pm.MoveInput.x > 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Right Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        //     else if (_pm.MoveInput.x < 0) {
        //         if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left Down").position, _checkerRadius, _terrainMask)) {
        //             Vector3 noTerrainPosition = CurrentChunk.transform.Find("Left Down").position;
        //             SpawnChunk(noTerrainPosition);
        //         }
        //     }
        // }      
    }

    private void SpawnChunk(Vector3 pointPosition) {
        int rand = Random.Range(0, _terrainChunks.Count);
        GameObject latestChunk = Instantiate(_terrainChunks[rand], pointPosition, Quaternion.identity);
        _spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimazation() {
        // _opCooldown -= Time.deltaTime;
        // if (_opCooldown <= 0f) {
        //     _opCooldown = _opCooldonwDuration;
        // } else {
        //     return;
        // }
        foreach (GameObject chunk in _spawnedChunks) {
            if (Vector2.Distance(chunk.transform.position, Player.transform.position) >= _maxOpDist) {
                chunk.SetActive(false);
            } else {
                chunk.SetActive(true);
            }
        }
    }
}
